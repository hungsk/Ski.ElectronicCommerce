<template>
  <div class="page-title-wrapper">
    <img :src="path" alt="標題圖片" class="page-img"/>
    <div class="page-title-layer py-3 px-2 px-md-5 py-md-4">
      <h1 class="h2"><slot></slot></h1>
    </div>
  </div>
</template>

<script>

export default {
  name: 'PageTitle',
  props: ['path']
}
</script>
