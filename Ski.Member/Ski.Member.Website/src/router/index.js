import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Front/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    children: [
      {
        path: '/',
        name: 'Index',
        component: () => import('../views/Front/Index.vue')
      }
    ]
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: { name: 'Index' }
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes,
  linkExactActiveClass: 'active',
  scrollBehavior () {
    return {
      left: 0, top: 0
    }
  }
})

export default router
