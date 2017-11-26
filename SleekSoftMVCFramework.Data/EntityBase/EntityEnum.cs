using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.EntityBase
{
    public enum EnTransferType
    {
        [Description("Signup Default Credit ")]
        FreeSignUp_Credit = 1,
        [Description("Paid")]
        Bank_Credit
    }


    public enum EnPaymentMethod
    {
        [Description("Signup Default Credit ")]
        Bank = 1,
        [Description("Paid")]
        Cash,
        Mobile,
        [Description("Me-To-U")]
        MeToyou
    }

    public enum EnProcessChannel
    {
        [Description("Website")]
        Website = 1,
        [Description("Standalone")]
        Standalone,
        [Description("Mobile")]
        Mobile,
    }

    public enum AcccountType
    {
        ReSeller = 1,
        Individual,
    }

    public enum EnNotificationChannel
    {
        [Description("Email")]
        Email = 1,
        [Description("SMS")]
        SMS,
        [Description("Both")]
        Both
    }

    public enum EnAlertFrequency
    {
        [Description("Per Transaction")]
        PerTransaction = 1,
        [Description("Monthly")]
        Monthly,
        [Description("Yearly")]
        Yearly,
    }

    public enum EnSMSType
    {
        [Description("Simple")]
        Simple = 1,
        [Description("Turbo")]
        Turbo,
        [Description("Classic")]
        Classic,
        [Description("Professional")]
        Professional,
    }

    public enum EnSMSStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Successful")]
        Successful,
        [Description("Failed")]
        Failed,
        [Description("Cancelled")]
        Cancelled,
    }

    public enum CreditTransferStatus
    {
        Sucessfully = 1,
        pending,
        Failed
    }

}
