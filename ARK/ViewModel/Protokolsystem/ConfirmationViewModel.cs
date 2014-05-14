﻿using System.Windows.Input;

namespace ARK.ViewModel.Protokolsystem
{
    public class ConfirmationViewModelBase : ProtokolsystemContentViewModelBase
    {
        public ICommand CommandHide
        {
            get { return GetCommand(e => Hide()); }
        }

        public virtual void Hide()
        {
            ProtocolSystem.HideDialog();
        }
    }
}
