<template>
  <nav aria-label="Page navigation">
    <ul class="pagination justify-content-center">
      <li class="page-item" :class="{'disabled' : !pageItem.has_pre}">
        <a class="page-link" href="#" @click.prevent="updatePage(Number(pageItem.current_page) - 1)">&laquo;</a>
      </li>
      <li class="page-item" v-for="item in pageItem.total_pages" :key="item"
      :class="{'active':pageItem.current_page===item}">
        <a class="page-link" href="#" @click.prevent="updatePage(item)">{{ item }}</a>
      </li>
      <li class="page-item" :class="{'disabled' : !pageItem.has_next}">
        <a class="page-link" href="#" @click.prevent="updatePage(Number(pageItem.current_page) + 1)">&raquo;</a>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'BackPagination',
  props: ['pageItem'],
  methods: {
    updatePage (page) {
      this.$emit('updatePage', page)
    }
  }
}
</script>
