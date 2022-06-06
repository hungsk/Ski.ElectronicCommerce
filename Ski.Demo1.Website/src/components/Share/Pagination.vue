<template>
  <nav aria-label="Page navigation">
    <ul class="pagination justify-content-center">
      <li class="page-item" :class="{'disabled' : !pageItem.hasPre}">
        <a class="page-link" href="#" @click.prevent="getPages(Number(pageItem.currentPage)-1)">&laquo;</a>
      </li>
      <li class="page-item" v-if="pageItem.pageTagHasPre">
        <a href="#" class="page-link" @click.prevent="getPages((pageItem.currentPageTag-1)*pageItem.showPage)">...</a>
      </li>
      <li class="page-item" v-for="item in pageItem.pageCurrent" :key="item"
      :class="{'active':pageItem.currentPage===item}">
        <a class="page-link" href="#" @click.prevent="getPages(item)">{{ item }}</a>
      </li>
      <li class="page-item" v-if="pageItem.pageTagHasNext">
        <a href="#" class="page-link" @click.prevent="getPages((pageItem.currentPageTag+1)*pageItem.showPage)">...</a>
      </li>
      <li class="page-item" :class="{'disabled' : !pageItem.hasNext}">
        <a class="page-link" href="#" @click.prevent="getPages(Number(pageItem.currentPage)+1)">&raquo;</a>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'Pagination',
  props: {
    pageItem: {
      type: Object
    }
  },
  methods: {
    getPages (page) {
      this.$emit('changePage', page)
    }
  }
}
</script>
