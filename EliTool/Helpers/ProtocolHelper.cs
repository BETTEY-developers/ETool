using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace EliTool.Helpers;

// author: https://www.yanning.wang/archives/469.html

/// <summary>
/// 协议辅助类
/// <para>在Windows Vista及以上平台调用时，请注意先申请UAC管理员权限。</para>
/// </summary>
public static class ProtocolHelper
{
    #region 判断指定协议名是否有效 + public static bool IsProtocol(string protocolName)
    /// <summary>
    /// 判断指定协议名是否有效
    /// </summary>
    /// <param name="protocolName">协议名称</param>
    /// <returns>判断结果</returns>
    public static bool IsProtocol(string protocolName)
    {
        bool result = false;

        RegistryKey protocolKey = Registry.ClassesRoot.OpenSubKey(protocolName);
        if (protocolKey != null)
        {
            if (protocolKey.GetValue("URL Protocol") != null)
                result = true;

            protocolKey.Dispose();
        }

        return result;
    }
    #endregion

    #region 判断指定协议名是否存在 + public static bool ProtocolNameExists(string protocolName)
    /// <summary>
    /// 判断指定协议名是否存在
    /// </summary>
    /// <param name="protocolName">协议名称</param>
    /// <returns>判断结果</returns>
    public static bool ProtocolNameExists(string protocolName)
    {
        bool result = false;

        RegistryKey protocolKey = Registry.ClassesRoot.OpenSubKey(protocolName);
        if (protocolKey != null)
        {
            result = true;
            protocolKey.Dispose();
        }

        return result;
    }
    #endregion

    #region 向当前系统申请注册协议 + public static void Register(string protocolName, string applicationPath)
    /// <summary>
    /// 向当前系统申请注册协议
    /// <para>类似于“协议名://”；仅限于Windows平台。</para>
    /// </summary>
    /// <param name="protocolName">协议名</param>
    /// <param name="applicationPath">可执行程序路径</param>
    public static void Register(string protocolName, string applicationPath)
    {
        if (ProtocolNameExists(protocolName))
            throw new InvalidOperationException("此协议名已被占用。");

        RegistryKey protocolKey = Registry.ClassesRoot.CreateSubKey(protocolName);
        protocolKey.SetValue(string.Empty, string.Format("URL:{0} Protocol", protocolName));
        protocolKey.SetValue("URL Protocol", applicationPath);

        RegistryKey defaultIconKey = protocolKey.CreateSubKey("DefaultIcon");
        defaultIconKey.SetValue(string.Empty, string.Format("{0},1", applicationPath));
        defaultIconKey.Dispose();

        RegistryKey shellKey = protocolKey.CreateSubKey("shell");
        RegistryKey openKey = shellKey.CreateSubKey("open");
        RegistryKey commandKey = openKey.CreateSubKey("command");

        commandKey.SetValue(string.Empty, string.Format("\"{0}\" \"%1\"", applicationPath));

        commandKey.Dispose();
        openKey.Dispose();
        shellKey.Dispose();

        protocolKey.Dispose();
    }
    #endregion

    #region 取消协议注册 + public static void UnRegister(string protocolName)
    /// <summary>
    /// 取消协议注册
    /// <para>注意：此方法将直接删除HKEY_CLASSES_ROOT根下对应子键。</para>
    /// </summary>
    /// <param name="protocolName">协议名</param>
    public static void UnRegister(string protocolName)
    {
        if (!ProtocolNameExists(protocolName))
            return;
        else if (!IsProtocol(protocolName))
            throw new InvalidOperationException("协议名无效。");

        //删除协议键
        Registry.ClassesRoot.DeleteSubKeyTree(protocolName);
    }
    #endregion
}
