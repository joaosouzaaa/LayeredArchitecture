﻿using FlowerShop.Domain.Enums;
using System.ComponentModel;

namespace FlowerShop.Business.Extensions;
public static class MessageExtension
{
    public static string Description(this EMessage message)
    {
        var memberInfo = typeof(EMessage).GetMember(message.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return ((DescriptionAttribute)attributes[0]).Description;
    }
}
