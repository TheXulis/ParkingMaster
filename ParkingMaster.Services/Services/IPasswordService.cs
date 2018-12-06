﻿using System;


namespace ParkingMaster.Services
{
    interface IPasswordService
    {
        string Sha1Hash(string pw);
        string HashPassword(string pw, byte[] salt);
        byte[] GetSalt();
        int CheckIfPasswordBreached(string pw);
        
    }
}
