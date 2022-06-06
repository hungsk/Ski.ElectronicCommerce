<template>
  <div class="container">
    <div class="row">
      <div class="col-12 py-5 text-center">
        <h1>商品管理</h1>
      </div>
      <div class="col-12 d-flex justify-content-between py-3">
        <div>
          <div class="input-group">
            <input type="text" v-model="searchText" class="form-control"/>
            <button type="button" class="btn btn-secondary btn-hv-style" @click="searchProduct">
              搜尋
            </button>
          </div>
        </div>
        <button type="button" class="btn btn-primary btn-hv-style" @click="openModal(true)">
          新增商品
        </button>
      </div>
      <div class="col-12 pb-5">
        <nav class="navbar navbar-expand navbar-front navbar-front-hr" aria-label="navbar">
          <div class="container-fluid">
            <div class="collapse navbar-collapse">
              <ul class="navbar-nav me-auto w-100 justify-content-between align-items-center">
                <li class="nav-item">
                  <a class="nav-link active" aria-current="page" href="#"
                  @click.prevent="getSelectKind('全部')">全部</a>
                </li>
                <li class="nav-item dropdown position-static">
                  <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown"
                  role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  建材
                  </a>
                  <ul class="dropdown-menu w-100 shadow rounded" aria-labelledby="navbarDropdown">
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('大理石')">大理石</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('木皮')">木皮</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('表面材')">表面材</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('磁磚')">磁磚</a>
                    </li>
                  </ul>
                </li>
                <li class="nav-item dropdown position-static">
                  <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                  data-bs-toggle="dropdown" aria-expanded="false">
                  地材
                  </a>
                  <ul class="dropdown-menu w-100 shadow rounded" aria-labelledby="navbarDropdown">
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('木地板')">木地板</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('石塑地板')">石塑地板</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('塑膠地板')">塑膠地板</a>
                    </li>
                  </ul>
                </li>
                <li class="nav-item dropdown position-static">
                  <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                  data-bs-toggle="dropdown" aria-expanded="false">
                  壁紙/塗料
                  </a>
                  <ul class="dropdown-menu w-100 shadow rounded" aria-labelledby="navbarDropdown">
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('壁紙')">壁紙</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('油漆')">油漆</a>
                    </li>
                    <li class="mx-3 my-2">
                      <a class="dropdown-item mb-0" href="#" @click.prevent="getSelectKind('特殊漆')">特殊漆</a>
                    </li>
                  </ul>
                </li>
              </ul>
            </div>
          </div>
        </nav>
      </div>
      <div class="col-12">
        <div class="table-responsive">
          <table class="table" id="table-product">
            <thead>
              <tr>
                <th>名稱</th>
                <th>種類</th>
                <th>是否啟用</th>
                <th>原價</th>
                <th>特價</th>
                <th>編輯</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in productsTable" :key="item.id">
                <td>{{ item.title }}</td>
                <td>{{ item.category }}</td>
                <td>{{ item.is_enabled ? '啟用' : '未啟用' }}</td>
                <td>{{ $filters.currency(item.origin_price) }}</td>
                <td>{{ $filters.currency(item.price) }}</td>
                <td>
                  <button type="button" class="btn btn-outline-primary me-2" @click="openModal(false, item)">
                    修改
                  </button>
                  <button type="button" class="btn btn-outline-dark" @click="openDeleteModal(item)">
                    刪除
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <Pagination :pageItem="pageItem" :data="productsFilter" @changePage="goToPage"/>
      </div>
    </div>
    <MainModal ref="modalMain" :currentItem="currentProduct" @updateProduct="updateProduct">
      <template v-slot:title>{{ modalTitle }}</template>
    </MainModal>
     <DeleteModal ref="modalDelete" :currentItem="currentProduct" @deleteItem="deleteProduct">
      <template v-slot:title>{{ modalTitle }}</template>
    </DeleteModal>
  </div>
</template>

<script>
import Pagination from '@/components/Share/Pagination.vue'
import MainModal from '@/components/Back/ProductMainModal.vue'
import DeleteModal from '@/components/Back/DeleteModal.vue'

export default {
  name: 'ProductManage',
  components: {
    Pagination,
    MainModal,
    DeleteModal
  },
  data () {
    return {
      productsAll: [],
      productsTable: [],
      productsFilter: [],
      searchText: '',
      selectKind: '全部',
      modalTitle: '',
      isNew: true,
      currentProduct: {
        imagesUrl: []
      },
      pageItem: {
        pageTotal: 0,
        currentPage: 0,
        hasPre: true,
        hasNext: false,
        showPage: 10,
        pageCurrent: [],
        currentPageTag: 1,
        PageTagTotal: 0,
        pageTagHasPre: false,
        pageTagHasNext: false
      }
    }
  },
  methods: {
    getProducts (page = 1) {
      const vm = this
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/products/all`
      vm.emitter.emit('loading', true)
      vm.$http.get(api).then((res) => {
        if (res.data.success) {
          vm.productsAll = []
          const objectProducts = res.data.products
          for (const item in objectProducts) {
            vm.productsAll.push(objectProducts[item])
          }
          if (vm.selectKind === '全部') {
            vm.productsFilter = [...this.productsAll]
          } else {
            this.KindFilter(vm.selectKind)
          }
          const result = vm.$pagination(vm.productsFilter, vm.productsTable, page)
          vm.pageItem = result[0]
          vm.productsTable = result[1]
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知193',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知202',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    searchProduct () {
      this.productsFilter = this.productsAll.filter((item) => item.title.includes(this.searchText))
      const result = this.$pagination(this.productsFilter, this.productsTable)
      this.pageItem = result[0]
      this.productsTable = result[1]
    },
    KindFilter (kind) {
      switch (kind) {
        case '大理石':
          this.productsFilter = this.productsAll.filter((item) => item.category === '大理石')
          break
        case '木皮':
          this.productsFilter = this.productsAll.filter((item) => item.category === '木皮')
          break
        case '木地板':
          this.productsFilter = this.productsAll.filter((item) => item.category === '木地板')
          break
        case '表面材':
          this.productsFilter = this.productsAll.filter((item) => item.category === '表面材')
          break
        case '磁磚':
          this.productsFilter = this.productsAll.filter((item) => item.category === '磁磚')
          break
        case '壁紙':
          this.productsFilter = this.productsAll.filter((item) => item.category === '壁紙')
          break
        case '油漆':
          this.productsFilter = this.productsAll.filter((item) => item.category === '油漆')
          break
        case '特殊漆':
          this.productsFilter = this.productsAll.filter((item) => item.category === '特殊漆')
          break
        case '絞鍊':
          this.productsFilter = this.productsAll.filter((item) => item.category === '絞鍊')
          break
        case '滑軌':
          this.productsFilter = this.productsAll.filter((item) => item.category === '滑軌')
          break
        case '門鎖/門把':
          this.productsFilter = this.productsAll.filter((item) => item.category === '門鎖/門把')
          break
        case '其他':
          this.productsFilter = this.productsAll.filter((item) => item.category === '其他')
          break
        case '石塑地板':
          this.productsFilter = this.productsAll.filter((item) => item.category === '石塑地板')
          break
        case '塑膠地板':
          this.productsFilter = this.productsAll.filter((item) => item.category === '塑膠地板')
          break
      }
    },
    getSelectKind (kind) {
      this.selectKind = kind
      if (kind === '全部') {
        this.productsFilter = [...this.productsAll]
      }
      this.KindFilter(kind)
      const result = this.$pagination(this.productsFilter, this.productsTable)
      this.pageItem = result[0]
      this.productsTable = result[1]
    },
    openModal (isNew, item) {
      if (isNew) {
        this.currentProduct = { imagesUrl: [] }
        this.modalTitle = '新增產品'
      } else {
        this.currentProduct = { ...item }
        this.currentProduct.imagesUrl.length = 5
        this.modalTitle = '修改產品'
      }
      this.isNew = isNew
      this.$refs.modalMain.showModal()
    },
    openDeleteModal (item) {
      this.modalTitle = '刪除商品'
      this.currentProduct = { ...item }
      this.$refs.modalDelete.showModal()
    },
    updateProduct (item) {
      console.log('updateProduct')
      console.log(item)
      const vm = this
      let api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/product`
      let httpMethod = 'post'
      if (!this.isNew) {
        api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/product/${item.id}`
        httpMethod = 'put'
      }
      vm.$refs.modalMain.hideModal()
      vm.emitter.emit('loading', true)
      vm.$http[httpMethod](api, { data: item }).then((res) => {
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知',
            content: '商品更新成功',
            icon: 'fas fa-check-circle'
          })
          vm.getProducts(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知310',
            content: res.data.message.join('、'),
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知319',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    deleteProduct (id) {
      const vm = this
      const token = document.cookie.replace(/(?:(?:^|.*;\s*)hexToken\s*=\s*([^;]*).*$)|^.*$/, '$1')
      vm.$http.defaults.headers.common.Authorization = `Bearer ${token}`
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/product/${id}`
      console.log(api)
      vm.$refs.modalDelete.hideModal()
      vm.emitter.emit('loading', true)
      vm.$http.delete(api).then((res) => {
        console.log('deleteProduct')
        if (res.data.success) {
          vm.emitter.emit('push-message', {
            style: 'success',
            title: '訊息通知335',
            content: '商品刪除成功',
            icon: 'fas fa-check-circle'
          })
          vm.getProducts(vm.pageItem.currentPage)
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知343',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
        vm.emitter.emit('loading', false)
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知352',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    goToPage (page) {
      const result = this.$pagination(this.productsFilter, this.productsTable, page)
      this.pageItem = result[0]
      this.productsTable = result[1]
    }
  },
  created () {
    this.getProducts()
  }
}
</script>
