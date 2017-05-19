﻿namespace Bonbonniere.Services.ServiceModels
{
    public class SignInResult
    {
        private static readonly SignInResult _success = new SignInResult { Succeeded = true };
        private static readonly SignInResult _failed = new SignInResult();
        private static readonly SignInResult _lockedOut = new SignInResult { IsLockedOut = true };
        private static readonly SignInResult _notAllowed = new SignInResult { IsNotAllowed = true };

        public bool Succeeded { get; protected set; }
        public bool IsLockedOut { get; protected set; }
        public bool IsNotAllowed { get; protected set; }

        public static SignInResult Success => _success;
        public static SignInResult Failed => _failed;
        public static SignInResult LockedOut => _lockedOut;
        public static SignInResult NotAllowed => _notAllowed;

        public override string ToString()
        {
            return IsLockedOut ? "LockedOut" :
                                IsNotAllowed ? "IsNotAllowed" :
                                Succeeded ? "Succeeded" : "Failed";
        }
    }
}
