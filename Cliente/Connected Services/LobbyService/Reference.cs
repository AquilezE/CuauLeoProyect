﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.LobbyService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/Servicio")]
    [System.SerializableAttribute()]
    public partial class Message : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LobbyCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
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
        public string LobbyCode {
            get {
                return this.LobbyCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.LobbyCodeField, value) != true)) {
                    this.LobbyCodeField = value;
                    this.RaisePropertyChanged("LobbyCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LobbyService.ILobby", CallbackContract=typeof(Cliente.LobbyService.ILobbyCallback))]
    public interface ILobby {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/SendMessage")]
        void SendMessage(Cliente.LobbyService.Message mensaje);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(Cliente.LobbyService.Message mensaje);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/Connect", ReplyAction="http://tempuri.org/ILobby/ConnectResponse")]
        bool Connect(string lobbyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/Connect", ReplyAction="http://tempuri.org/ILobby/ConnectResponse")]
        System.Threading.Tasks.Task<bool> ConnectAsync(string lobbyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/Disconnect", ReplyAction="http://tempuri.org/ILobby/DisconnectResponse")]
        bool Disconnect(string lobbyCode, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/Disconnect", ReplyAction="http://tempuri.org/ILobby/DisconnectResponse")]
        System.Threading.Tasks.Task<bool> DisconnectAsync(string lobbyCode, string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/GetMessage")]
        void GetMessage(Cliente.LobbyService.Message message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/JoinLobby", ReplyAction="http://tempuri.org/ILobby/JoinLobbyResponse")]
        bool JoinLobby();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveLobby", ReplyAction="http://tempuri.org/ILobby/LeaveLobbyResponse")]
        bool LeaveLobby();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyChannel : Cliente.LobbyService.ILobby, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyClient : System.ServiceModel.DuplexClientBase<Cliente.LobbyService.ILobby>, Cliente.LobbyService.ILobby {
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SendMessage(Cliente.LobbyService.Message mensaje) {
            base.Channel.SendMessage(mensaje);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(Cliente.LobbyService.Message mensaje) {
            return base.Channel.SendMessageAsync(mensaje);
        }
        
        public bool Connect(string lobbyCode, string username) {
            return base.Channel.Connect(lobbyCode, username);
        }
        
        public System.Threading.Tasks.Task<bool> ConnectAsync(string lobbyCode, string username) {
            return base.Channel.ConnectAsync(lobbyCode, username);
        }
        
        public bool Disconnect(string lobbyCode, string username) {
            return base.Channel.Disconnect(lobbyCode, username);
        }
        
        public System.Threading.Tasks.Task<bool> DisconnectAsync(string lobbyCode, string username) {
            return base.Channel.DisconnectAsync(lobbyCode, username);
        }
    }
}
