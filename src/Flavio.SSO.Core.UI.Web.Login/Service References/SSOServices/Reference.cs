﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flavio.SSO.Core.UI.Web.Login.SSOServices {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserViewModel", Namespace="http://schemas.datacontract.org/2004/07/Flavio.SSO.Core.Application.ViewModels")]
    [System.SerializableAttribute()]
    public partial class UserViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExpirationTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Flavio.SSO.Core.UI.Web.Login.SSOServices.GroupViewModel[] GroupsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LogonTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ExpirationTime {
            get {
                return this.ExpirationTimeField;
            }
            set {
                if ((this.ExpirationTimeField.Equals(value) != true)) {
                    this.ExpirationTimeField = value;
                    this.RaisePropertyChanged("ExpirationTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Flavio.SSO.Core.UI.Web.Login.SSOServices.GroupViewModel[] Groups {
            get {
                return this.GroupsField;
            }
            set {
                if ((object.ReferenceEquals(this.GroupsField, value) != true)) {
                    this.GroupsField = value;
                    this.RaisePropertyChanged("Groups");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LogonTime {
            get {
                return this.LogonTimeField;
            }
            set {
                if ((this.LogonTimeField.Equals(value) != true)) {
                    this.LogonTimeField = value;
                    this.RaisePropertyChanged("LogonTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GroupViewModel", Namespace="http://schemas.datacontract.org/2004/07/Flavio.SSO.Core.Application.ViewModels")]
    [System.SerializableAttribute()]
    public partial class GroupViewModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SSOServices.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticatedUserIsValid", ReplyAction="http://tempuri.org/IUserService/AuthenticatedUserIsValidResponse")]
        bool AuthenticatedUserIsValid(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticatedUserIsValid", ReplyAction="http://tempuri.org/IUserService/AuthenticatedUserIsValidResponse")]
        System.Threading.Tasks.Task<bool> AuthenticatedUserIsValidAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticateUser", ReplyAction="http://tempuri.org/IUserService/AuthenticateUserResponse")]
        Flavio.SSO.Core.UI.Web.Login.SSOServices.UserViewModel AuthenticateUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticateUser", ReplyAction="http://tempuri.org/IUserService/AuthenticateUserResponse")]
        System.Threading.Tasks.Task<Flavio.SSO.Core.UI.Web.Login.SSOServices.UserViewModel> AuthenticateUserAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/LogOffUser", ReplyAction="http://tempuri.org/IUserService/LogOffUserResponse")]
        void LogOffUser(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/LogOffUser", ReplyAction="http://tempuri.org/IUserService/LogOffUserResponse")]
        System.Threading.Tasks.Task LogOffUserAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : Flavio.SSO.Core.UI.Web.Login.SSOServices.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<Flavio.SSO.Core.UI.Web.Login.SSOServices.IUserService>, Flavio.SSO.Core.UI.Web.Login.SSOServices.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool AuthenticatedUserIsValid(string token) {
            return base.Channel.AuthenticatedUserIsValid(token);
        }
        
        public System.Threading.Tasks.Task<bool> AuthenticatedUserIsValidAsync(string token) {
            return base.Channel.AuthenticatedUserIsValidAsync(token);
        }
        
        public Flavio.SSO.Core.UI.Web.Login.SSOServices.UserViewModel AuthenticateUser(string userName, string password) {
            return base.Channel.AuthenticateUser(userName, password);
        }
        
        public System.Threading.Tasks.Task<Flavio.SSO.Core.UI.Web.Login.SSOServices.UserViewModel> AuthenticateUserAsync(string userName, string password) {
            return base.Channel.AuthenticateUserAsync(userName, password);
        }
        
        public void LogOffUser(string token) {
            base.Channel.LogOffUser(token);
        }
        
        public System.Threading.Tasks.Task LogOffUserAsync(string token) {
            return base.Channel.LogOffUserAsync(token);
        }
    }
}
