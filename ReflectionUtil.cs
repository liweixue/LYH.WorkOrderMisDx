using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace LYH.WorkOrder
{
    public sealed class ReflectionUtil
    {
        // Fields
        public static BindingFlags Bf = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        // Methods
        private ReflectionUtil()
        {
        }

        public static object CreateInstance(string type)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (var i = 0; i < assemblies.Length; i++)
            {
                if (assemblies[i].GetType(type) != null)
                {
                    return assemblies[i].CreateInstance(type);
                }
            }
            return null;
        }

        public static object CreateInstance(Type type)
        {
            return CreateInstance(type.FullName);
        }

        public static object GetAttribute(Type attributeType, Assembly assembly)
        {
            bool flag;

            {
                if (attributeType == null)
                {
                    throw new ArgumentNullException(nameof(attributeType));
                }
                if (assembly == null)
                {
                    throw new ArgumentNullException(nameof(assembly));
                }
                flag = !assembly.IsDefined(attributeType, false);
            }
            if (!flag)
            {
                return assembly.GetCustomAttributes(attributeType, false)[0];
            }
            return null;
        }

        public static object GetAttribute(Type attributeType, MemberInfo type)
        {
            return GetAttribute(attributeType, type, false);
        }

        public static object GetAttribute(Type attributeType, MemberInfo type, bool searchParent)
        {
            if (attributeType != null)
            {
                if (type == null)
                {
                    return null;
                }
                if (!attributeType.IsSubclassOf(typeof(Attribute)))
                {
                    return null;
                }
                if (type.IsDefined(attributeType, searchParent))
                {
                    var customAttributes = type.GetCustomAttributes(attributeType, searchParent);
                    if (customAttributes.Length > 0)
                    {
                        return customAttributes[0];
                    }
                }
            }
            return null;
        }

        public static object[] GetAttributes(Type attributeType, MemberInfo type)
        {
            return GetAttributes(attributeType, type, false);
        }

        public static object[] GetAttributes(Type attributeType, MemberInfo type, bool searchParent)
        {
            bool flag;

            {
                if (type == null)
                {
                    return null;
                }
                if (attributeType == null)
                {
                    return null;
                }
                flag = attributeType.IsSubclassOf(typeof(Attribute));
            }
            if (flag && type.IsDefined(attributeType, false))
            {
                return type.GetCustomAttributes(attributeType, searchParent);
            }
            return null;
        }

        public static string GetDescription(Enum value)
        {
            return GetDescription(value, null);
        }

        public static string GetDescription(MemberInfo member)
        {
            return GetDescription(member, null);
        }

        public static string GetDescription(Enum value, params object[] args)
        {
            DescriptionAttribute[] customAttributes;
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            customAttributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            var format = (customAttributes.Length > 0) ? customAttributes[0].Description : value.ToString();
            if ((args != null) && (args.Length > 0))
            {
                return string.Format(null, format, args);
            }
            return format;
        }

        public static string GetDescription(MemberInfo member, params object[] args)
        {
            string description;

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if (!member.IsDefined(typeof(DescriptionAttribute), false))
            {
                return string.Empty;
            }
            var customAttributes = (DescriptionAttribute[])member.GetCustomAttributes(typeof(DescriptionAttribute), false);
            description = customAttributes[0].Description;
            if ((args != null) && (args.Length > 0))
            {
                return string.Format(null, description, args);
            }
            return description;
        }

        public static object GetField(object obj, string name)
        {
            return obj.GetType().GetField(name, Bf).GetValue(obj);
        }

        public static FieldInfo[] GetFields(object obj)
        {
            return obj.GetType().GetFields(Bf);
        }

        public static Stream GetImageResource(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        public static string GetManifestString(Type assemblyType, string charset, string resName)
        {
            var manifestResourceStream = Assembly.GetAssembly(assemblyType).GetManifestResourceStream(
                $"{assemblyType.Namespace}.{resName.Replace("/", ".")}");
            if (manifestResourceStream == null)
            {
                return "";
            }
            var length = (int)manifestResourceStream.Length;
            var buffer = new byte[length];
            manifestResourceStream.Read(buffer, 0, length);
            return ((buffer != null) ? Encoding.GetEncoding(charset).GetString(buffer) : "");
        }

        public static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties(Bf);
        }

        public static object GetProperty(object obj, string name)
        {
            return obj.GetType().GetProperty(name, Bf).GetValue(obj, null);
        }

        public static string GetStringRes(Type assemblyType, string resName, string resourceHolder)
        {
            var assembly = Assembly.GetAssembly(assemblyType);
            var manager = new ResourceManager(resourceHolder, assembly);
            return manager.GetString(resName);
        }

        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            return obj.GetType().InvokeMember(methodName, Bf | BindingFlags.InvokeMethod, null, obj, args);
        }

        public static Bitmap LoadBitmap(Type assemblyType, string resourceHolder, string imageName)
        {
            var assembly = Assembly.GetAssembly(assemblyType);
            var manager = new ResourceManager(resourceHolder, assembly);
            return (Bitmap)manager.GetObject(imageName);
        }

        public static void SetField(object obj, string name, object value)
        {
            obj.GetType().GetField(name, Bf).SetValue(obj, value);
        }

        public static void SetProperty(object obj, string name, object value)
        {
            var property = obj.GetType().GetProperty(name, Bf);
            value = Convert.ChangeType(value, property.PropertyType);
            property.SetValue(obj, value, null);
        }

        public static string ToNameValuePairs(object obj, bool includeEmptyProperties = true)
        {
            var str = "";
            foreach (var info in obj.GetType().GetProperties())
            {
                var obj2 = info.GetValue(obj, null);
                var str2 = (obj2 != null) ? obj2.ToString() : null;
                if (!string.IsNullOrEmpty(str2))
                {
                    goto Label_0096;
                }
                if (!includeEmptyProperties)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(str))
                {
                    goto Label_007A;
                }
                str = str + "&";
            Label_007A:
                str = str + $"{info.Name}={str2}";
                continue;
            Label_0096:
                if (!string.IsNullOrEmpty(str))
                {
                    str = str + "&";
                }
                str = str + $"{info.Name}={str2}";
            }
            return str;
        }
    }
}
