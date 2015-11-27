﻿// -----------------------------------------------------------------------
//  <copyright file="AssemblyExtensions.cs" company="柳柳软件">
//      Copyright (c) 2014 66SOFT. All rights reserved.
//  </copyright>
//  <site>http://www.66soft.net</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-09-08 7:46</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ShiYi.Utility
{
    /// <summary>
    /// 程序集扩展操作类
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取程序集的文件版本
        /// </summary>
        public static Version GetFileVersion(this Assembly assembly)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.FileVersion);
        }

        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        public static Version GetProductVersion(this Assembly assembly)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.ProductVersion);
        }
    }
}