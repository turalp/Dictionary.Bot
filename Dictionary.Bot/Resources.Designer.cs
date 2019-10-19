﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dictionary.Bot {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Dictionary.Bot.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sözün izahını axtarılan zaman səhv oldu. Mesajı təkrar göndərin. .
        /// </summary>
        internal static string ExceptionMessage {
            get {
                return ResourceManager.GetString("ExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sözün izahını almaq üçün /e, /explain, /i, /izah [söz] sintaksisi istifadə edin..
        /// </summary>
        internal static string ExplainCommandMessage {
            get {
                return ResourceManager.GetString("ExplainCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sözün izahılı almaq üçün onu göndərməyə yetərlidir..
        /// </summary>
        internal static string ExplainMessage {
            get {
                return ResourceManager.GetString("ExplainMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Botun istifadə qaydalarını baxmaq üçün /h, /help, /k, /kömək yazın..
        /// </summary>
        internal static string HelpMessage {
            get {
                return ResourceManager.GetString("HelpMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Söz məlumat bazasında tapılmadı..
        /// </summary>
        internal static string NoWordMessage {
            get {
                return ResourceManager.GetString("NoWordMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Azərbaycan izahlı lüğət botuna xoş gəlmişsiniz. Bir sözün izahını tapmaq üçün sözü bota göndərməniz yetərlidir..
        /// </summary>
        internal static string StartMessage {
            get {
                return ResourceManager.GetString("StartMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mesajın növü yalnışdır..
        /// </summary>
        internal static string TypeMismatchMessage {
            get {
                return ResourceManager.GetString("TypeMismatchMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Söz tapılmadı. Bəlkə istədiyiz söz bu siyahıda mövcüddur:\n.
        /// </summary>
        internal static string WordMismatchMessage {
            get {
                return ResourceManager.GetString("WordMismatchMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sözün izahını almaq üçün ancaq bir söz göndərməlisiniz..
        /// </summary>
        internal static string WrongCountMessage {
            get {
                return ResourceManager.GetString("WrongCountMessage", resourceCulture);
            }
        }
    }
}
