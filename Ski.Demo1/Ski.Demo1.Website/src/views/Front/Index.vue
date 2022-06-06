<template>
  <div class="container">
    <Carousel/>
    <Menu :active="'首頁'"/>
    <HeaderMain class="pt-5">
      <template v-slot:headerMain>DEMO空間OOOO</template>
      <template v-slot:content>
        <p>提供豐富多元的OO建材資訊</p>
        <p>配合友善的介面，讓建材資訊公開透明</p>
      </template>
    </HeaderMain>
    <div class="py-5 row g-0">
      <div class="col-md-4 mt-3 mb-0 my-md-3">
        <router-link to="/new" class="text-decoration-none">
          <div class="img-link animated">
            <img v-lazy="require('@/assets/images/menu-img-v.jpg')" alt="新品到貨" class="img-menu-v">
            <div class="menu-item-title verticle-line">
              <span>新品到貨</span>
            </div>
          </div>
        </router-link>
      </div>
      <div class="col-md-8 mt-0 mb-3 my-md-3">
        <div class="row">
          <div class="col-12">
            <router-link to="/discount" class="text-decoration-none">
              <div class="img-link animated">
                <img v-lazy="require('@/assets/images/menu-img.png')" alt="限時優惠" class="img-menu">
                <div class="menu-item-title">限時優惠</div>
              </div>
            </router-link>
          </div>
          <div class="col-12">
            <router-link to="/article" class="text-decoration-none">
              <div class="img-link animated">
                <img v-lazy="require('@/assets/images/menu-img-h2.jpg')" alt="私房設計推薦" class="img-menu">
                <div class="menu-item-title">私房設計推薦</div>
              </div>
            </router-link>
          </div>
        </div>
      </div>
    </div>
    <div class="py-5">
     <div class="row">
      <div class="col-md-4 text-center px-md-5 px-3 mb-3">
        <img :src="require('@/assets/images/packages.png')" alt="多元齊全" width="150" class="img-fluid my-3">
        <h3 class="my-3">多元齊全</h3>
        <p>擁有多種類的各式建材，讓設計師、廠商、客戶都能找到理想建設素材</p>
      </div>
      <div class="col-md-4 text-center px-md-5 px-3 mb-3">
        <img :src="require('@/assets/images/search-price.png')" alt="公開透明" width="150" class="img-fluid my-3">
        <h3 class="my-3">公開透明</h3>
        <p>一目瞭然的商品價格資訊，讓您不用詢問，輕鬆比價找到理想建材</p>
      </div>
      <div class="col-md-4 text-center px-md-5 px-3 mb-3">
        <img :src="require('@/assets/images/need.png')" alt="貼近需求" width="150" class="img-fluid my-3">
        <h3 class="my-3">貼近需求</h3>
        <p>在DEMO空間您可以找到品質優良的多樣建材，滿足您對室內設計的需求，打造理想舒適的家</p>
      </div>
     </div>
    </div>
    <div class="py-5">
      <Header>
        <template v-slot:header>熱賣商品</template>
      </Header>
      <div class="py-3">
        <swiper :slidesPerView="4" :spaceBetween="20" class="swiper-container-index animated"
        :slidesPerGroup="4" :loop="true" :autoHeight="true"
        :navigation="true" :lazy="true"
        :breakpoints="swiper.breakpoints">
        <swiper-slide v-for="item in ItemJson" :key="item.id">
          <router-link :to="`/product/${item.id}`">
            <img :src="item.imageUrl" class="swiper-lazy swiper-main-img"/>
            <img :src="item.imageUrl2" class="swiper-lazy swiper-second-img"/>
            <div class="swiper-lazy-preloader swiper-lazy-preloader-white"></div>
              <div class="view-more">
                <span class="h5">查看更多</span>
              </div>
              <div class="slide-captions">
                <p class="text-start mb-0">{{ item.title }}</p>
                <p class="text-start mb-0"><small>{{ item.description }}</small></p>
              </div>
            </router-link>
          </swiper-slide>
        </swiper>
      </div>
    </div>
    <div class="py-5">
      <div class="row justify-content-center my-3">
        <div class="col-md-8">
          <div class="d-flex px-3 py-3 align-items-center justify-content-center">
            <img :src="require('@/assets/images/garland.png')" alt="開幕歡慶" width="100" class="img-fluid">
            <div class="px-3 px-md-5">
              <p class="h3">開幕歡慶</p>
              <p>輸入代號
                <span class="text-success fw-bold">&nbsp;happybuy123&nbsp;</span>即享九折優惠
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="py-5">
      <Header>
        <template v-slot:header>聯絡我們</template>
      </Header>
      <div class="contact row g-0 py-3">
        <div class="col-md-6 animated">
          <iframe crossorigin="anonymous"
          src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1807.109428940167!2d121.57541290794208!3d25.06057022569894!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3442ab13a340b6b5%3A0x14c3f84217cb6ab0!2zSUtFQSDlrpzlrrblrrblsYUg5YWn5rmW5bqX!5e0!3m2!1szh-TW!2stw!4v1624536578867!5m2!1szh-TW!2stw&output=embed"
          width="100%" height="450" style="border:0;"></iframe>
        </div>
        <div class="col-md-6 bg-secondary animated">
          <div class="contact-wrapper p-4 my-5">
            <Form @submit="sendMessage" v-slot="{ errors }">
              <div class="form-floating mb-3">
                <Field type="text" name="姓名" class="form-control" id="userName" placeholder="姓名"
                :class="{ 'is-invalid': errors['姓名'] }" rules="required"
                v-model="user.name"></Field>
                <label for="userName" v-if="!errors['姓名']">姓名</label>
                <ErrorMessage as="label" for="userName" name="姓名" class="invalid-feedback"></ErrorMessage>
              </div>
              <div class="form-floating mb-3">
                <Field type="email" class="form-control" name="email" id="email" placeholder="name@example.com"
                :class="{ 'is-invalid': errors['email'] }" rules="email|required" v-model="user.email"></Field>
                <label for="email" v-if="!errors['email']">Email</label>
                <ErrorMessage as="label" for="email" name="email" class="invalid-feedback"></ErrorMessage>
              </div>
              <div class="form-floating mb-3">
                <Field as="textarea" class="form-control" rules="required" placeholder="留言"
                v-model="user.msg" id="msg" name="留言" style="height: 100px"
                :class="{ 'is-invalid': errors['留言'] }">
                </Field>
                <label for="msg" v-if="!errors['留言']">留言</label>
                <ErrorMessage name="留言" as="label" for="msg" class="invalid-feedback"></ErrorMessage>
              </div>
              <div class="text-end">
                <button class="btn-major btn w-100">送出</button>
              </div>
            </Form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Carousel from '@/components/Front/Carousel.vue'
import Menu from '@/components/Front/Menu.vue'
import Header from '@/components/Front/Header.vue'
import HeaderMain from '@/components/Front/HeaderMain.vue'
import $ from 'jquery'
import SwiperCore, { Navigation } from 'swiper'
import { Swiper, SwiperSlide } from 'swiper/vue'
import 'swiper/swiper.scss'
import 'swiper/components/navigation/navigation.scss'
import ItemJson from '@/assets/json/IndexProduct.json'
SwiperCore.use([Navigation])

export default {
  name: 'Index',
  components: {
    Carousel,
    Menu,
    Header,
    HeaderMain,
    Swiper,
    SwiperSlide
  },
  data () {
    return {
      ItemJson,
      user: {
        name: '',
        email: '',
        msg: ''
      },
      userLogin: {
        username: '',
        password: ''
      },
      swiper: {
        breakpoints: {
          0: {
            slidesPerView: 1,
            spaceBetween: 10,
            slidesPerGroup: 1
          },
          567: {
            slidesPerView: 1,
            spaceBetween: 10,
            slidesPerGroup: 1
          },
          768: {
            slidesPerView: 3,
            spaceBetween: 20,
            slidesPerGroup: 3
          },
          1024: {
            slidesPerView: 4,
            spaceBetween: 20,
            slidesPerGroup: 4
          }
        }
      }
    }
  },
  computed: {
    isCompleted () {
      return this.user.name && this.user.email && this.user.msg
    }
  },
  methods: {
    scroll () {
      const scrollPos = $(window).scrollTop()
      const windowH = $(window).height()
      $('.animated').each((index, element) => {
        const thisPos = $(element).offset().top
        if ((windowH + scrollPos) >= thisPos) {
          $(element).addClass('slidein')
        }
      })
    },
    sendMessage (values, { resetForm }) {
      this.emitter.emit('push-message', {
        style: 'success',
        title: '訊息通知',
        content: '已收到您的留言，感謝您的支持與建議!',
        icon: 'fas fa-check-circle'
      })
      resetForm()
    },
    login () {
      const vm = this
      const api = `${process.env.VUE_APP_API}/user/signin`
      vm.emitter.emit('loading', true)
      vm.userLogin.username = 'guest'
      vm.userLogin.password = 'guest'
      vm.$http.post(api, vm.userLogin).then((res) => {
        console.log(res.data)
        if (res.data.success) {
          const { token, expired } = res.data
          document.cookie = `hexToken=${token};expires=${new Date(expired)};`
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '登入成功',
            icon: 'fas fa-check-circle'
          })
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知257',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知266',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    }
  },
  created () {
    const vm = this
    vm.emitter.emit('cartVisible', true)
    window.addEventListener('scroll', this.scroll)
    vm.emitter.emit('loading', true)
    window.setTimeout(() => {
      vm.emitter.emit('loading', false)
    }, 2000)
    this.login()
  },
  unmounted () {
    window.removeEventListener('scroll', this.scroll)
  }
}
</script>
