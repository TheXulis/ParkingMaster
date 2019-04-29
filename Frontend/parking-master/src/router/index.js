import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home'
import Login from '@/views/Login'
import Reservation from '@/views/Reservation'
import LotRegistration from '@/views/LotRegistration'
import LotDeletion from '@/views/LotDeletion'
import VehicleRegistration from '@/views/VehicleRegistration'
import ParkingLotDashboard from '@/views/ParkingLotDashboard'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      redirect: '/Home'
    },
    {
      path: '/Home',
      name: 'Home',
      component: Home
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login,
      props: (route) => ({token: route.query.token})
    },
    {
      path: '/Reservation',
      name: 'Reservation',
      component: Reservation
    },
    {
      path: '/LotRegistration',
      name: 'lotRegistration',
      component: LotRegistration
    },
    {
      path: '/LotDeletion',
      name: 'lotDeletion',
      component: LotDeletion
    },
    {
      path: '/VehicleRegistration',
      name: 'vehicleRegistration',
      component: VehicleRegistration
    },
    {
      path: '/ParkingLotDashboard',
      name: 'parkingLotDashboard',
      component: ParkingLotDashboard
    }
  ]
})
