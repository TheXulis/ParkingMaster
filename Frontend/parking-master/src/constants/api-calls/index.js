const PARKINGMASTER_BASE_URL = 'http://localhost:52324'
const SSO_BASE_URL = 'http://localhost:61348'
const SSO_FRONTEND_URL = 'http://localhost:61348'
const HELLO_WORLD = PARKINGMASTER_BASE_URL + '/api/testing'
const LOT_REGISTRATION = PARKINGMASTER_BASE_URL + '/api/lot/register'
const LOT_DELETION = PARKINGMASTER_BASE_URL + '/api/lot/delete'
const VEHICLE_REGISTRATION = PARKINGMASTER_BASE_URL + '/api/vehicle/register'
const SESSION_CHECK = PARKINGMASTER_BASE_URL + '/api/user/session'
const SSO_LOGIN = SSO_BASE_URL + '/api/users/login'
const DELETE_FROM_APPS = PARKINGMASTER_BASE_URL + '/api/user/deleteallapps'
const GET_ALL_LOTS = PARKINGMASTER_BASE_URL + '/api/lotManagement/getAllLots'
const GET_ALL_LOTS_BY_OWNER = PARKINGMASTER_BASE_URL + '/api/lotManagement/getAllLotsByOwner'
const GET_ALL_SPOTS_FOR_LOT = PARKINGMASTER_BASE_URL + '/api/lotManagement/getSpotsByLot'
const GET_ALL_USER_VEHICLES = PARKINGMASTER_BASE_URL + '/api/vehicle/getAllUserVehicles'
const RESERVE_PARKING_SPOT = PARKINGMASTER_BASE_URL + '/api/reservation/add'
const LOGOUT = PARKINGMASTER_BASE_URL + '/api/user/logout'
const ROLE_SELECTION = PARKINGMASTER_BASE_URL + '/api/user/setrole'
const GET_ALL_RESERVATIONS = PARKINGMASTER_BASE_URL + '/api/reservation/getall'
const EXTEND_RESERVATION = PARKINGMASTER_BASE_URL + '/api/reservation/extend'
const GET_CURRENT_TOS = PARKINGMASTER_BASE_URL + '/api/tos/current'
const ACCEPT_TOS = PARKINGMASTER_BASE_URL + '/api/tos/useraccepted'
const SSO_LANDING = SSO_FRONTEND_URL + '/#/landing'
const GET_ALL_LOGS = PARKINGMASTER_BASE_URL + '/api/admin/logs'

export default {
  BASE_URL: PARKINGMASTER_BASE_URL,
  HELLO_WORLD: HELLO_WORLD,
  LOT_REGISTRATION,
  LOT_DELETION,
  VEHICLE_REGISTRATION,
  SSO_LOGIN,
  SESSION_CHECK,
  DELETE_FROM_APPS,
  GET_ALL_LOTS,
  GET_ALL_LOTS_BY_OWNER,
  GET_ALL_SPOTS_FOR_LOT,
  RESERVE_PARKING_SPOT,
  GET_ALL_USER_VEHICLES,
  SSO_FRONTEND_URL,
  LOGOUT,
  ROLE_SELECTION,
  GET_ALL_RESERVATIONS,
  EXTEND_RESERVATION,
  GET_CURRENT_TOS,
  ACCEPT_TOS,
  SSO_LANDING,
  GET_ALL_LOGS
}
