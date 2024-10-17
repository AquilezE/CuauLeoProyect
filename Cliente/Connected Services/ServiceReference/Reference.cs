﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDto", Namespace="http://schemas.datacontract.org/2004/07/BevososService")]
    [System.SerializableAttribute()]
    public partial class UserDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ProfilePictureIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
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
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProfilePictureId {
            get {
                return this.ProfilePictureIdField;
            }
            set {
                if ((this.ProfilePictureIdField.Equals(value) != true)) {
                    this.ProfilePictureIdField = value;
                    this.RaisePropertyChanged("ProfilePictureId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IUsersManager")]
    public interface IUsersManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsUsernameTaken", ReplyAction="http://tempuri.org/IUsersManager/IsUsernameTakenResponse")]
        bool IsUsernameTaken(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsUsernameTaken", ReplyAction="http://tempuri.org/IUsersManager/IsUsernameTakenResponse")]
        System.Threading.Tasks.Task<bool> IsUsernameTakenAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsEmailTaken", ReplyAction="http://tempuri.org/IUsersManager/IsEmailTakenResponse")]
        bool IsEmailTaken(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/IsEmailTaken", ReplyAction="http://tempuri.org/IUsersManager/IsEmailTakenResponse")]
        System.Threading.Tasks.Task<bool> IsEmailTakenAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/SendToken", ReplyAction="http://tempuri.org/IUsersManager/SendTokenResponse")]
        bool SendToken(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/SendToken", ReplyAction="http://tempuri.org/IUsersManager/SendTokenResponse")]
        System.Threading.Tasks.Task<bool> SendTokenAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/VerifyToken", ReplyAction="http://tempuri.org/IUsersManager/VerifyTokenResponse")]
        bool VerifyToken(string email, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/VerifyToken", ReplyAction="http://tempuri.org/IUsersManager/VerifyTokenResponse")]
        System.Threading.Tasks.Task<bool> VerifyTokenAsync(string email, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/RegisterUser", ReplyAction="http://tempuri.org/IUsersManager/RegisterUserResponse")]
        bool RegisterUser(string email, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/RegisterUser", ReplyAction="http://tempuri.org/IUsersManager/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(string email, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/LogIn", ReplyAction="http://tempuri.org/IUsersManager/LogInResponse")]
        Cliente.ServiceReference.UserDto LogIn(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersManager/LogIn", ReplyAction="http://tempuri.org/IUsersManager/LogInResponse")]
        System.Threading.Tasks.Task<Cliente.ServiceReference.UserDto> LogInAsync(string email, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersManagerChannel : Cliente.ServiceReference.IUsersManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersManagerClient : System.ServiceModel.ClientBase<Cliente.ServiceReference.IUsersManager>, Cliente.ServiceReference.IUsersManager {
        
        public UsersManagerClient() {
        }
        
        public UsersManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsUsernameTaken(string username) {
            return base.Channel.IsUsernameTaken(username);
        }
        
        public System.Threading.Tasks.Task<bool> IsUsernameTakenAsync(string username) {
            return base.Channel.IsUsernameTakenAsync(username);
        }
        
        public bool IsEmailTaken(string email) {
            return base.Channel.IsEmailTaken(email);
        }
        
        public System.Threading.Tasks.Task<bool> IsEmailTakenAsync(string email) {
            return base.Channel.IsEmailTakenAsync(email);
        }
        
        public bool SendToken(string email) {
            return base.Channel.SendToken(email);
        }
        
        public System.Threading.Tasks.Task<bool> SendTokenAsync(string email) {
            return base.Channel.SendTokenAsync(email);
        }
        
        public bool VerifyToken(string email, string token) {
            return base.Channel.VerifyToken(email, token);
        }
        
        public System.Threading.Tasks.Task<bool> VerifyTokenAsync(string email, string token) {
            return base.Channel.VerifyTokenAsync(email, token);
        }
        
        public bool RegisterUser(string email, string username, string password) {
            return base.Channel.RegisterUser(email, username, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(string email, string username, string password) {
            return base.Channel.RegisterUserAsync(email, username, password);
        }
        
        public Cliente.ServiceReference.UserDto LogIn(string email, string password) {
            return base.Channel.LogIn(email, password);
        }
        
        public System.Threading.Tasks.Task<Cliente.ServiceReference.UserDto> LogInAsync(string email, string password) {
            return base.Channel.LogInAsync(email, password);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ILobbyManager", CallbackContract=typeof(Cliente.ServiceReference.ILobbyManagerCallback))]
    public interface ILobbyManager {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/NewLobbyCreated")]
        void NewLobbyCreated(Cliente.ServiceReference.UserDto userDto);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/NewLobbyCreated")]
        System.Threading.Tasks.Task NewLobbyCreatedAsync(Cliente.ServiceReference.UserDto userDto);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/JoinLobby")]
        void JoinLobby(int lobbyId, Cliente.ServiceReference.UserDto userDto);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/JoinLobby")]
        System.Threading.Tasks.Task JoinLobbyAsync(int lobbyId, Cliente.ServiceReference.UserDto userDto);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/SendMessage")]
        void SendMessage(int lobbyId, int UserId, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(int lobbyId, int UserId, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/LeaveLobby")]
        void LeaveLobby(int lobbyId, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/LeaveLobby")]
        System.Threading.Tasks.Task LeaveLobbyAsync(int lobbyId, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/KickUser")]
        void KickUser(int lobbyId, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobbyManager/KickUser")]
        System.Threading.Tasks.Task KickUserAsync(int lobbyId, int UserId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/OnNewLobbyCreated", ReplyAction="http://tempuri.org/ILobbyManager/OnNewLobbyCreatedResponse")]
        void OnNewLobbyCreated(int lobbyId, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/OnJoinLobby", ReplyAction="http://tempuri.org/ILobbyManager/OnJoinLobbyResponse")]
        void OnJoinLobby(int lobbyId, Cliente.ServiceReference.UserDto userDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/OnLeaveLobby", ReplyAction="http://tempuri.org/ILobbyManager/OnLeaveLobbyResponse")]
        void OnLeaveLobby(int lobbyId, int UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/OnSendMessage", ReplyAction="http://tempuri.org/ILobbyManager/OnSendMessageResponse")]
        void OnSendMessage(int UserId, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyManager/OnLobbyUsersUpdate", ReplyAction="http://tempuri.org/ILobbyManager/OnLobbyUsersUpdateResponse")]
        void OnLobbyUsersUpdate(int lobbyId, Cliente.ServiceReference.UserDto[] users);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyManagerChannel : Cliente.ServiceReference.ILobbyManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyManagerClient : System.ServiceModel.DuplexClientBase<Cliente.ServiceReference.ILobbyManager>, Cliente.ServiceReference.ILobbyManager {
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void NewLobbyCreated(Cliente.ServiceReference.UserDto userDto) {
            base.Channel.NewLobbyCreated(userDto);
        }
        
        public System.Threading.Tasks.Task NewLobbyCreatedAsync(Cliente.ServiceReference.UserDto userDto) {
            return base.Channel.NewLobbyCreatedAsync(userDto);
        }
        
        public void JoinLobby(int lobbyId, Cliente.ServiceReference.UserDto userDto) {
            base.Channel.JoinLobby(lobbyId, userDto);
        }
        
        public System.Threading.Tasks.Task JoinLobbyAsync(int lobbyId, Cliente.ServiceReference.UserDto userDto) {
            return base.Channel.JoinLobbyAsync(lobbyId, userDto);
        }
        
        public void SendMessage(int lobbyId, int UserId, string message) {
            base.Channel.SendMessage(lobbyId, UserId, message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(int lobbyId, int UserId, string message) {
            return base.Channel.SendMessageAsync(lobbyId, UserId, message);
        }
        
        public void LeaveLobby(int lobbyId, int UserId) {
            base.Channel.LeaveLobby(lobbyId, UserId);
        }
        
        public System.Threading.Tasks.Task LeaveLobbyAsync(int lobbyId, int UserId) {
            return base.Channel.LeaveLobbyAsync(lobbyId, UserId);
        }
        
        public void KickUser(int lobbyId, int UserId) {
            base.Channel.KickUser(lobbyId, UserId);
        }
        
        public System.Threading.Tasks.Task KickUserAsync(int lobbyId, int UserId) {
            return base.Channel.KickUserAsync(lobbyId, UserId);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ILobbyChecker")]
    public interface ILobbyChecker {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyChecker/IsLobbyOpen", ReplyAction="http://tempuri.org/ILobbyChecker/IsLobbyOpenResponse")]
        bool IsLobbyOpen(int lobbyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyChecker/IsLobbyOpen", ReplyAction="http://tempuri.org/ILobbyChecker/IsLobbyOpenResponse")]
        System.Threading.Tasks.Task<bool> IsLobbyOpenAsync(int lobbyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyChecker/IsLobbyFull", ReplyAction="http://tempuri.org/ILobbyChecker/IsLobbyFullResponse")]
        bool IsLobbyFull(int lobbyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobbyChecker/IsLobbyFull", ReplyAction="http://tempuri.org/ILobbyChecker/IsLobbyFullResponse")]
        System.Threading.Tasks.Task<bool> IsLobbyFullAsync(int lobbyId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyCheckerChannel : Cliente.ServiceReference.ILobbyChecker, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyCheckerClient : System.ServiceModel.ClientBase<Cliente.ServiceReference.ILobbyChecker>, Cliente.ServiceReference.ILobbyChecker {
        
        public LobbyCheckerClient() {
        }
        
        public LobbyCheckerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LobbyCheckerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyCheckerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyCheckerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool IsLobbyOpen(int lobbyId) {
            return base.Channel.IsLobbyOpen(lobbyId);
        }
        
        public System.Threading.Tasks.Task<bool> IsLobbyOpenAsync(int lobbyId) {
            return base.Channel.IsLobbyOpenAsync(lobbyId);
        }
        
        public bool IsLobbyFull(int lobbyId) {
            return base.Channel.IsLobbyFull(lobbyId);
        }
        
        public System.Threading.Tasks.Task<bool> IsLobbyFullAsync(int lobbyId) {
            return base.Channel.IsLobbyFullAsync(lobbyId);
        }
    }
}
