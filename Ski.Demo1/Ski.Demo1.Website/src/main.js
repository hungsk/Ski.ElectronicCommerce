import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'
import VueAxios from 'vue-axios'
import lazyPlugin from 'vue3-lazy'
import 'bootstrap'
import 'bootstrap-icons/font/bootstrap-icons.css'
import mitt from 'mitt'
import { VueMasonryPlugin } from 'vue-masonry/src/masonry-vue3.plugin'
import {
  Form, Field, ErrorMessage, defineRule, configure
} from 'vee-validate'
import AllRules from '@vee-validate/rules'
import { localize, setLocale } from '@vee-validate/i18n'
import zhTW from '@vee-validate/i18n/dist/locale/zh_TW.json'
import Loading from 'vue3-loading-overlay'
import 'vue3-loading-overlay/dist/vue3-loading-overlay.css'
import { currency, date } from './methods/filter.js'
import { pagination } from './methods/pagination.js'
import VueSweetalert2 from 'vue-sweetalert2'
import 'sweetalert2/dist/sweetalert2.min.css'
// vue masonry
const emitterInit = mitt()

// lazyload
const lazyPluginSetting = {
  loading: require('@/assets/images/loading.png'),
  error: require('@/assets/images/loading.png')
}
// vee-validate
Object.keys(AllRules).forEach((rule) => {
  defineRule(rule, AllRules[rule])
})

configure({
  generateMessage: localize({ zh_TW: zhTW }),
  validateOnInput: true
})
setLocale('zh_TW')

const app = createApp(App)
app.config.globalProperties.emitter = emitterInit
app.config.globalProperties.$filters = { currency, date }
app.config.globalProperties.$pagination = pagination
app.use(store)
app.use(router)
app.use(lazyPlugin, lazyPluginSetting)
app.use(VueAxios, axios)
app.use(VueMasonryPlugin)
app.use(VueSweetalert2)
app.component('Loading', Loading)
app.component('Form', Form)
app.component('Field', Field)
app.component('ErrorMessage', ErrorMessage)
app.mount('#app')
