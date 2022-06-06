<template>
  <div class="modal fade" ref="modal" id="productModal" tabindex="-1" aria-labelledby="productModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-fullscreen">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title" id="productModalLabel">
            <slot name="title"></slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal"
          aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="row">
            <div class="col-sm-4">
              <div class="mb-3">
                <label for="image" class="form-label">*輸入封面圖片網址</label>
                <input type="text" class="form-control" id="image" v-model="currentProduct.imageUrl"
                placeholder="請輸入圖片連結" >
              </div>
              <div class="mb-3">
                <label for="customFile" class="form-label">或 上傳封面圖片
                  <i class="fas fa-spinner fa-spin"></i>
                </label>
                <input type="file" id="customFile" class="form-control" ref="imageUpload1"
                @change="uploadFile">
              </div>
              <img class="img-fluid" alt="產品圖片" :src="currentProduct.imageUrl">
            </div>
            <div class="col-sm-8">
              <div class="mb-3">
                <label for="title" class="form-label">*標題</label>
                <input type="text" class="form-control" id="title" v-model="currentProduct.title"
                placeholder="請輸入標題">
              </div>
              <div class="row gx-2">
                <div class="mb-3 col-md-6">
                  <label for="category" class="form-label">*分類</label>
                  <input type="text" class="form-control" id="category" v-model="currentProduct.category"
                  placeholder="請輸入分類">
                </div>
                <div class="mb-3 col-md-6">
                  <label for="price" class="form-label">*單位</label>
                  <input type="text" class="form-control" id="unit" v-model="currentProduct.unit"
                  placeholder="請輸入單位">
                </div>
              </div>
              <div class="row gx-2">
                <div class="mb-3 col-md-6">
                  <label for="origin_price" class="form-label">*原價</label>
                  <input type="number" class="form-control" id="origin_price"
                  v-model.number="currentProduct.origin_price" placeholder="請輸入原價">
                </div>
                <div class="mb-3 col-md-6">
                  <label for="price" class="form-label">*售價</label>
                  <input type="number" class="form-control" id="price"
                  v-model.number="currentProduct.price" placeholder="請輸入售價">
                </div>
              </div>
              <hr>
              <div class="mb-3">
                <label for="description" class="form-label">*產品描述</label>
                <textarea type="text" class="form-control" id="description"
                v-model="currentProduct.description" placeholder="請輸入產品描述"></textarea>
              </div>
              <div class="mb-3">
                <label for="content" class="form-label">*說明內容</label>
                <textarea type="text" class="form-control" id="content" v-model="currentProduct.content"
                placeholder="請輸入產品說明內容"></textarea>
              </div>
              <div class="mb-3">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox"
                  :true-value="1" :false-value="0" id="is_enabled" v-model="currentProduct.is_enabled">
                  <label class="form-check-label" for="is_enabled">
                  *是否啟用
                  </label>
                </div>
              </div>
            </div>
            <div class="col-12">
              <p>請上傳至少一張圖片*</p>
              <div class="row">
                <div class="my-3 col-6">
                  <div class="mb-3">
                    <input type="url" class="form-control" v-model="image1">
                    <input type="file" id="customFile" ref="itemImages1" class="form-control"
                    @change="uploadFileMultiple($event, 1)">
                  </div>
                  <img :src="currentProduct.imagesUrl[0]" alt="產品圖" class="img-fluid"
                  v-if="currentProduct.imagesUrl">
                  <img v-else :src="image1" alt="產品圖" class="img-fluid">
                </div>
                <div class="my-3 col-6">
                  <div class="mb-3">
                    <input type="url" class="form-control" v-model="image2">
                    <input type="file" id="customFile" ref="itemImages2" class="form-control"
                    @change="uploadFileMultiple($event, 2)">
                  </div>
                  <img :src="currentProduct.imagesUrl[1]" alt="產品圖" class="img-fluid"
                  v-if="currentProduct.imagesUrl">
                  <img v-else :src="image2" alt="產品圖" class="img-fluid">
                </div>
                <div class="my-3 col-6">
                  <div class="mb-3">
                    <input type="url" class="form-control" v-model="image3">
                    <input type="file" id="customFile" ref="itemImages3" class="form-control"
                    @change="uploadFileMultiple($event, 3)">
                  </div>
                  <img :src="currentProduct.imagesUrl[2]" alt="產品圖" class="img-fluid"
                  v-if="currentProduct.imagesUrl">
                  <img v-else :src="image3" alt="產品圖" class="img-fluid">
                </div>
                <div class="my-3 col-6">
                  <div class="mb-3">
                    <input type="url" class="form-control" v-model="image4">
                    <input type="file" id="customFile" ref="itemImages4" class="form-control"
                    @change="uploadFileMultiple($event, 4)">
                  </div>
                  <img :src="currentProduct.imagesUrl[3]" alt="產品圖" class="img-fluid"
                  v-if="currentProduct.imagesUrl">
                  <img v-else :src="image4" alt="產品圖" class="img-fluid">
                </div>
                <div class="my-3 col-6">
                  <div class="mb-3">
                    <input type="url" class="form-control" v-model="image5">
                    <input type="file" id="customFile" ref="itemImages5" class="form-control"
                    @change="uploadFileMultiple($event, 5)">
                  </div>
                  <img :src="currentProduct.imagesUrl[4]" alt="產品圖" class="img-fluid"
                  v-if="currentProduct.imagesUrl">
                  <img v-else :src="image5" alt="產品圖" class="img-fluid">
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark btn-hv-style" data-bs-dismiss="modal">
            取消
          </button>
          <button type="button" class="btn btn-primary btn-hv-style" @click="updateProduct(currentProduct)">
            儲存
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import modalMixin from '@/mixin/ModalMixin.js'
import firebase from 'firebase/app'
import 'firebase/storage'

const firebaseConfig = {
  apiKey: 'AIzaSyB5KJW2ar4drnpjLMbDVK_fhR9ACfNyqQ8',
  authDomain: 'creativespace-3e448.firebaseapp.com',
  projectId: 'creativespace-3e448',
  storageBucket: 'creativespace-3e448.appspot.com',
  messagingSenderId: '761960943306',
  appId: '1:761960943306:web:4d165f79b65c770abc6e33',
  measurementId: 'G-XVMN7L9QQP'
}

firebase.initializeApp(firebaseConfig)

export default {
  name: 'ProductMainModal',
  mixins: [modalMixin],
  props: {
    currentItem: {
      type: Object,
      default () { return {} }
    }
  },
  data () {
    return {
      modal: {},
      currentProduct: {},
      image1: '',
      image2: '',
      image3: '',
      image4: '',
      image5: ''
    }
  },
  methods: {
    uploadFile () {
      const vm = this
      const formData = new FormData()
      const api = `${process.env.VUE_APP_API}/api/${process.env.VUE_APP_APIPATH}/admin/upload`
      const uploadFile = this.$refs.imageUpload1.files[0]
      formData.append('file-to-upload', uploadFile)
      vm.$http.post(api, formData).then((res) => {
        if (res.data.success) {
          vm.currentProduct.imageUrl = res.data.imageUrl
        } else {
          vm.emitter.emit('push-message', {
            style: 'danger',
            title: '錯誤通知200',
            content: res.data.message,
            icon: 'fas fa-exclamation-circle'
          })
        }
      }).catch((err) => {
        vm.emitter.emit('push-message', {
          style: 'danger',
          title: '錯誤通知208',
          content: err,
          icon: 'fas fa-exclamation-circle'
        })
        vm.emitter.emit('loading', false)
      })
    },
    uploadFileMultiple (event, index) {
      const vm = this
      const el = `image${index}`
      const file = event.target.files[0]
      const storageRef = firebase.storage().ref(`productImages/${file.name}`)
      storageRef.put(file).then((res) => {
        storageRef.getDownloadURL().then((url) => {
          vm[el] = url
        })
      })
    },
    updateProduct (currentProduct) {
      if (this.currentProduct.imagesUrl[0] === undefined) {
        this.currentProduct.imagesUrl.splice(0, 1)
      }
      if (this.currentProduct.imagesUrl[1] === undefined) {
        this.currentProduct.imagesUrl.splice(1, 1)
      }
      if (this.currentProduct.imagesUrl[2] === undefined) {
        this.currentProduct.imagesUrl.splice(2, 1)
      }
      if (this.currentProduct.imagesUrl[3] === undefined) {
        this.currentProduct.imagesUrl.splice(3, 1)
      }
      if (this.currentProduct.imagesUrl[4] === undefined) {
        this.currentProduct.imagesUrl.splice(4, 1)
      }

      this.$emit('updateProduct', currentProduct)
      this.image1 = ''
      this.image2 = ''
      this.image3 = ''
      this.image4 = ''
      this.image5 = ''
      this.$refs.itemImages1.value = ''
      this.$refs.itemImages2.value = ''
      this.$refs.itemImages3.value = ''
      this.$refs.itemImages4.value = ''
      this.$refs.itemImages5.value = ''
    }
  },
  watch: {
    currentItem () {
      this.currentProduct = this.currentItem
    },
    image1 () {
      this.currentProduct.imagesUrl[0] = this.image1
    },
    image2 () {
      this.currentProduct.imagesUrl[1] = this.image2
    },
    image3 () {
      this.currentProduct.imagesUrl[2] = this.image3
    },
    image4 () {
      this.currentProduct.imagesUrl[3] = this.image4
    },
    image5 () {
      this.currentProduct.imagesUrl[4] = this.image5
    }
  }
}
</script>
