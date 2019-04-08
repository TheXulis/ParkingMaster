// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import router from './router'
import App from './App'
import Vuetify, {
  VToolbar,
  VApp,
  VBtn,
  VTextField,
  VForm,
  VList,
  VNavigationDrawer,
  VToolbarTitle,
  VSpacer,
  VContent,
  VToolbarItems
} from 'vuetify/lib'
import 'vuetify/src/stylus/app.styl'

Vue.use(Vuetify, {
  iconfont: 'md',
  components: {
    VApp,
    VToolbar,
    VBtn,
    VTextField,
    VForm,
    VList,
    VNavigationDrawer,
    VToolbarTitle,
    VSpacer,
    VContent,
    VToolbarItems
  }
})

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
