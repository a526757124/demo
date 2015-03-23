﻿// -----------------------------------------------------------------------
//  <copyright file="Operator.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-08-12 19:24</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ETVS.Framework.Entity.Core
{
    /// <summary>
    /// 当前操作者信息类
    /// 注:要记录操作 所有用户都得继承此类
    /// </summary>
    public class Operator : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 当前用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置 当前用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 当前访问IP
        /// </summary>
        public string Ip { get; set; }
    }
}