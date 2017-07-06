using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcApplication1.Services
{
    public class PaymentService
    {
        public PaymentService()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        //Get the payment list of the current user
        public List<Subscription> GetAll()
        {
            List<Subscription> MySubs;
            using(var db = new SubscriptionContext())
            {
                MySubs = db.Subscription.Include("Payments").Where(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId)).ToList();
                
                foreach(var sub in MySubs)
                {
                    sub.CardNumber = "****-****-****-" + sub.CardNumber.Substring(15, 4);
                    sub.CVVCode = "***";
                }
                return MySubs;
            }
        }
        //Get the subscription of an user
        public Subscription GetSub(int id)
        {
            Subscription MySub;
            string CifratedNumber;
            using(var db = new SubscriptionContext())
            {
                MySub = db.Subscription.Find(id);
                CifratedNumber = "****-****-****-" + MySub.CardNumber.Substring(15, 4);
                MySub.CardNumber = CifratedNumber;
                MySub.CVVCode = "***";
                return MySub;
            }
        }

        //Check if the subscription has expired
        public void CheckSubscription(int id)
        {
            Subscription subscription;
            using (var db = new SubscriptionContext())
            {
                subscription = db.Subscription.Include("Payments").First(x => x.UserProfileId.Equals(id));

                switch (subscription.Status)
                {
                    case "inactive":
                        if (!HasOverduePayment(subscription))
                            ReactiveSubscription(id);
                        break;
                    case "active":
                        if (HasOverduePayment(subscription))
                            SetSubscriptionInactive(id);
                        else
                        {
                            if (!HasPendingPayment(subscription))
                                GenerateNextPayment();
                            if (subscription.ExpirationDate.CompareTo(DateTime.Now) < 1)
                                SuspendSubscription(subscription);
                        }
                        break;
                }
            }
        }

        public bool HasSub()
        {
            using(var db = new UsersContext())
            {
                UserProfile CurrentUser = db.UserProfiles.Include("Subscription").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                if (CurrentUser.Subscription == null)
                    return false;
                else
                    return true;
            }
        }

        //Check if the user has an active subscription
        public bool HasActiveSub()
        {
            using (var db = new UsersContext())
            {
                UserProfile CurrentUser = db.UserProfiles.Include("Subscription").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                if(CurrentUser.Subscription == null)
                {
                    return false;
                }
                if (CurrentUser.Subscription.Status.Equals("active") || CurrentUser.Subscription.Status.Equals("suspended"))
                    return true;
                else
                    return false;
            }
        }
        //Reactive the user subscription
        public void ReactiveSubscription(int id)
        {
            Subscription TargetSubscription;
            using(var db = new SubscriptionContext())
            {
                TargetSubscription = db.Subscription.Find(id);
                TargetSubscription.Status = "active";
                db.Entry(TargetSubscription).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SetSubscriptionInactive(int id)
        {
            Subscription TargetSubscription;
            using(var db = new SubscriptionContext())
            {
                TargetSubscription = db.Subscription.Find(id);
                TargetSubscription.Status = "inactive";
                db.Entry(TargetSubscription).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        //Update billing information of the user
        public void UpdatePaymentInfo(Subscription Model)
        {
            Subscription TargetSubscription;
            using(var db = new SubscriptionContext())
            {
                TargetSubscription = db.Subscription.Find(WebSecurity.CurrentUserId);
                TargetSubscription.BillingAddress = Model.BillingAddress;
                TargetSubscription.CardNumber = Model.CardNumber;
                TargetSubscription.CVVCode = Model.CVVCode;
                TargetSubscription.ExpirationDate = Model.ExpirationDate;
                TargetSubscription.Zip = Model.Zip;
                db.SaveChanges();
            }
        }

        //Set a subscription to suspend status (call when user cancel his subscription)
        public void SuspendSubscription(Subscription Model)
        {
            Subscription TargetSubscription;
            using(var db = new SubscriptionContext())
            {
                TargetSubscription = db.Subscription.Find(Model.UserProfileId);
                TargetSubscription.Status = "suspended";
                db.SaveChanges();
            }
        }

        //Set a subscription to inactive status
        public void CancelSubscription(Subscription Model)
        {
            Subscription TargetSubscription;
            using (var db = new SubscriptionContext())
            {
                TargetSubscription = db.Subscription.Find(Model.UserProfileId);
                TargetSubscription.Status = "inactive";
                db.SaveChanges();
            }
        }

        //Check for an overdue payment in a specific subscription
        public bool HasOverduePayment(Subscription Model)
        {
            CheckPaymentsStatus(Model);
            foreach(var item in Model.Payments)
            {
                if (item.Status == "overdue")
                    return true;
            }
            return false;
        }

        //Check for a pending payment in a specific subscription
        public bool HasPendingPayment(Subscription Model)
        {
            CheckPaymentsStatus(Model);
            foreach(var item in Model.Payments)
            {
                if (item.Status == "pending")
                    return true;
            }
            return false;
        }

        //Check payments status of an specific subscription
        public void CheckPaymentsStatus(Subscription Model)
        {
            foreach(var item in Model.Payments)
            {
                if(item.Date.CompareTo(DateTime.Now) < 1 && item.Status=="pending")
                {
                    item.Status = "overdue";
                }
            }
        }

        //Call this function when the user pays his pending invoices and reactives his subscription
        public void PayAllOverdues()
        {
            List<Payment> Payments;
            using(var db = new PaymentContext())
            {
                Payments = db.Payment.Where(x => x.SubscriptionId.Equals(WebSecurity.CurrentUserId) && x.Status.Equals("overdue")).ToList();
                
                foreach(var debt in Payments)
                {
                    debt.Status = "cancelled";
                    db.Entry(debt).State = EntityState.Modified;
                }
                db.SaveChanges();
                GenerateNextPayment();
                ReactiveSubscription(WebSecurity.CurrentUserId);
            }
        }

        //Generate the next payment to 30 days from todays date
        public void GenerateNextPayment()
        {
            Payment Next;
            Subscription Sub;
            using(var db = new PaymentContext())
            {
                Next = db.Payment.Create();
                Next.Amount = 15;
                Next.Date = DateTime.Now.AddDays(30);
                Next.Status = "pending";
                Sub = db.Subscription.Include("Payments").First(x => x.UserProfileId.Equals(WebSecurity.CurrentUserId));
                Sub.Payments.Add(Next);
                db.SaveChanges();
            }
        }

        //Register a new subscription
        public void CreateSub()
        {
            Subscription NewSubscription;

            using (var db = new SubscriptionContext())
            {
                NewSubscription = db.Subscription.Create();
                NewSubscription.Status = "active";
                NewSubscription.Payments = new List<Payment>();

                using (var db2 = new PaymentContext())
                {
                    Payment FirstPayment = db2.Payment.Create();
                    FirstPayment.Amount = 15.0f;
                    FirstPayment.Date = DateTime.Now;
                    FirstPayment.Status = "cancelled";
                  //  FirstPayment.Subscription = NewSubscription;
                  //  FirstPayment.SubscriptionId = NewSubscription.UserProfileId;
                    db2.Payment.Add(FirstPayment);


                    Payment SecondPayment = db2.Payment.Create();
                    SecondPayment.Amount = 15.0f;
                    SecondPayment.Date = DateTime.Now.AddDays(30.0);
                    SecondPayment.Status = "pending";
                 //   SecondPayment.Subscription = NewSubscription;
                 //   SecondPayment.SubscriptionId = NewSubscription.UserProfileId;
                    db2.Payment.Add(SecondPayment);


                    //Store subscription info
                    using (var db3 = new UsersContext())
                    {
                        UserProfile CurrentUser = db3.UserProfiles.Include("Subscription").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                        CurrentUser.Subscription = NewSubscription;
                        NewSubscription.Payments.Add(FirstPayment);
                        NewSubscription.Payments.Add(SecondPayment);
                        NewSubscription.UserProfile = CurrentUser;
                        NewSubscription.UserProfileId = CurrentUser.UserId;
                        db3.SaveChanges();
                    }
                    db2.SaveChanges();
                    db.Subscription.Add(NewSubscription);
                    db.SaveChanges();
                }
            }

        }

        public void CreateSub(FormCollection collection)
        {
            Subscription NewSubscription;

            using (var db = new SubscriptionContext())
            {
                NewSubscription = db.Subscription.Create();
                NewSubscription.Status = "active";
                NewSubscription.Payments = new List<Payment>();

                using (var db2 = new PaymentContext())
                {
                    Payment FirstPayment = db2.Payment.Create();
                    FirstPayment.Amount = 15.0f;
                    FirstPayment.Date = DateTime.Now;
                    FirstPayment.Status = "cancelled";
                    db2.Payment.Add(FirstPayment);


                    Payment SecondPayment = db2.Payment.Create();
                    SecondPayment.Amount = 15.0f;
                    SecondPayment.Date = DateTime.Now.AddDays(30.0);
                    SecondPayment.Status = "pending";
                    db2.Payment.Add(SecondPayment);


                    //Store subscription info
                    using (var db3 = new UsersContext())
                    {
                        UserProfile CurrentUser = db3.UserProfiles.Include("Subscription").First(x => x.UserId.Equals(WebSecurity.CurrentUserId));
                        CurrentUser.Subscription = NewSubscription;
                        NewSubscription.Payments.Add(FirstPayment);
                        NewSubscription.Payments.Add(SecondPayment);
                        NewSubscription.UserProfile = CurrentUser;
                        NewSubscription.UserProfileId = CurrentUser.UserId;
                        NewSubscription.CreditCard = collection["CreditCard"];
                        NewSubscription.CVVCode = collection["CVVCode"];
                        NewSubscription.CardNumber = collection["CardNumber"];
                        NewSubscription.Zip = collection["Zip"];
                        NewSubscription.BillingAddress = collection["BillingAddress"];
                        NewSubscription.Status = "active";
                        NewSubscription.ExpirationDate = DateTime.Parse(collection["ExpirationDate"]);
                        db3.SaveChanges();
                    }
               //     db2.SaveChanges();
               //     db.Subscription.Add(NewSubscription);
               //     db.SaveChanges();
                }
            }
        }
    }
}