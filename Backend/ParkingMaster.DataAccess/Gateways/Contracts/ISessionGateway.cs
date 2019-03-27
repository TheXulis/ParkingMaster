﻿using System;
using ParkingMaster.Models.DTO;


namespace ParkingMaster.DataAccess.Gateways.Contracts
{
    public interface ISessionGateway
    {
        ResponseDTO<SessionDTO> StoreSession(SessionDTO sessionDTO);
        ResponseDTO<SessionDTO> GetSession(Guid sessionId);
        ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);

    }
}