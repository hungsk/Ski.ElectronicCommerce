<template>
  <nav class="navbar navbar-expand-md navbar-front p-3">
    <div class="container">
      <div class="d-flex flex-column flex-md-row justify-content-md-between w-100">
        <div class="pb-2">
          <router-link class="navbar-brand d-flex align-items-center justify-content-center justify-content-md-start" to="/">
            <img src="@/assets/images/logo.png" alt="logo" class="me-2">
            <span class="fw-bold">DEMO空間</span>
          </router-link>
        </div>
        <div class="d-flex justify-content-center justify-content-md-end align-items-center pb-2">
          <router-link to="/favorite" class="me-4 menu-item">
            <i class="fas fa-heart fa-lg"></i>
            <span class="ms-2 menu-item-text">收藏</span>
          </router-link>
          <router-link to="/orderSearch" class="me-4 menu-item">
            <i class="fas fa-box"></i>
            <span class="ms-2 menu-item-text">訂單查詢</span>
          </router-link>
          <router-link to="/login" class="me-4 menu-item">
            <i class="fas fa-cogs fa-lg"></i>
            <span class="ms-2 menu-item-text">登入</span>
          </router-link>
          <a href="#" class="me-4 menu-item" @click.prevent="ShowSidebar">
            <i class="fas fa-shopping-cart fa-lg"></i>
            <span class="ms-2 menu-item-text">購物車</span>
          </a>
          <button class="navbar-toggler border-0 nav-toggler-style collapsed" ref="navbarToggler" type="button"
          data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <div class="btn-line"></div>
            <div class="btn-line"></div>
            <div class="btn-line"></div>
          </button>
        </div>
      </div>
      <div class="collapse navbar-collapse px-5 px-md-0" id="navbarSupportedContent" ref="collapseContent">
        <ul class="navbar-nav me-auto mb-2 d-md-none">
          <li class="nav-item">
            <router-link class="nav-link" aria-current="page" to="/" :class="{'active-item':activeItem ==='首頁'}"
            @click="activeItem='首頁';foldNav()">首頁</router-link>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" role="button" aria-expanded="false" :class="{'active-item':activeItem ==='建材'}"
            @click="activeItem='建材';switchDropdown($event)">
            建材
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown1">
              <li>
                <router-link class="dropdown-item" to="/product/material/大理石"
                @click="foldNav">大理石</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/material/木皮"
                @click="foldNav">木皮</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/material/表面材"
                @click="foldNav">表面材</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/material/磁磚"
                @click="foldNav">磁磚</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" role="button" aria-expanded="false"
            :class="{'active-item':activeItem ==='地材'}" @click="activeItem='地材';switchDropdown($event)">
              地材
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown2">
              <li>
                <router-link class="dropdown-item" to="/product/floor/木地板"
                @click="foldNav">木地板</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/floor/石塑地板"
                @click="foldNav">石塑地板</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/floor/塑膠地板"
                @click="foldNav">塑膠地板</router-link>
              </li>
            </ul>
          </li>
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle" role="button" aria-expanded="false"
            :class="{'active-item':activeItem ==='壁紙/塗料'}" @click="activeItem='壁紙/塗料';switchDropdown($event)">
              壁紙/塗料
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li>
                <router-link class="dropdown-item" to="/product/paint/壁紙"
                @click="foldNav">壁紙</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/paint/油漆"
                @click="foldNav">油漆</router-link>
              </li>
              <li>
                <router-link class="dropdown-item" to="/product/paint/特殊漆"
                @click="foldNav">特殊漆</router-link>
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
import $ from 'jquery'

export default {
  name: 'Navbar',
  data () {
    return {
      activeItem: '首頁',
      menuVisible: true
    }
  },
  methods: {
    foldNav () {
      this.$refs.navbarToggler.click()
    },
    ShowSidebar () {
      this.emitter.emit('openSidebar', true)
      if (this.$refs.collapseContent.classList.contains('show')) {
        this.foldNav()
      }
    },
    switchDropdown ($event) {
      $($event.target).parent().find('.dropdown-menu').toggleClass('show')
    }
  }
}
</script>
