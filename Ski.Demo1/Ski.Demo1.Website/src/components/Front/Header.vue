<template>
  <div class="container">
    <h2 class="header-deco mb-4 animated-x">
      <slot name="header"></slot>
    </h2>
    <div class="animated-x">
      <slot name="content"></slot>
    </div>
  </div>
</template>

<script>
import HeaderMixin from '@/mixin/HeaderMixin.js'

export default {
  name: 'Header',
  mixins: [HeaderMixin]
}
</script>
