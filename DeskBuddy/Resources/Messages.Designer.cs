﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeskBuddy.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DeskBuddy.Resources.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Desk Buddy.
        /// </summary>
        public static string ApplicationTitle {
            get {
                return ResourceManager.GetString("ApplicationTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exit.
        /// </summary>
        public static string Button_Exit {
            get {
                return ResourceManager.GetString("Button_Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to OK.
        /// </summary>
        public static string Button_Ok {
            get {
                return ResourceManager.GetString("Button_Ok", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reset.
        /// </summary>
        public static string Button_Reset {
            get {
                return ResourceManager.GetString("Button_Reset", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Settings.
        /// </summary>
        public static string Button_Settings {
            get {
                return ResourceManager.GetString("Button_Settings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save.
        /// </summary>
        public static string Settings_Save {
            get {
                return ResourceManager.GetString("Settings_Save", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sit Interval (minutes).
        /// </summary>
        public static string Settings_SitInterval {
            get {
                return ResourceManager.GetString("Settings_SitInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stand Interval (minutes).
        /// </summary>
        public static string Settings_StandInterval {
            get {
                return ResourceManager.GetString("Settings_StandInterval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please lower your desk and sit down..
        /// </summary>
        public static string Toast_TimeToSit_Message {
            get {
                return ResourceManager.GetString("Toast_TimeToSit_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time to Sit!.
        /// </summary>
        public static string Toast_TimeToSit_Title {
            get {
                return ResourceManager.GetString("Toast_TimeToSit_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please raise your desk and stand up..
        /// </summary>
        public static string Toast_TimeToStand_Message {
            get {
                return ResourceManager.GetString("Toast_TimeToStand_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time to Stand!.
        /// </summary>
        public static string Toast_TimeToStand_Title {
            get {
                return ResourceManager.GetString("Toast_TimeToStand_Title", resourceCulture);
            }
        }
    }
}
