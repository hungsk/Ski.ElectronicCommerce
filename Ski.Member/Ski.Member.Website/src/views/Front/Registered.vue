<template>
    <div class="canvasWrap">
        <div class="innerCanvas">
            <main>
                <form @submit.prevent="registered">
                    <div class="container">
                        <div class="pageTitle regist">會員註冊</div>
                    </div>
                    <div class="container">
                        <div class="row">
                            <div class="formSet offset-2 m-8 s-12 x-12">

                                <h3 class="formTitle">會員註冊</h3>

                                <div class="formField">

                                    <div class="fieldContent">
                                        <label for="Main_txtMemberId">會員帳號(身份證字號或居留證號)</label>
                                        <input name="ctl00$Main$txtMemberId" type="text" id="Main_txtMemberId"
                                            onkeydown="return (event.keyCode!=13);" placeholder="請輸入會員帳號(身份證字號或居留證號)"
                                            onkeyup="this.value=this.value.toUpperCase().trim();" />
                                    </div>

                                    <div class="fieldContent">
                                        <label for="Main_txtMemberName">會員姓名</label>
                                        <input name="ctl00$Main$txtMemberName" type="text" id="Main_txtMemberName"
                                            onkeydown="return (event.keyCode!=13);" placeholder="請輸入會員姓名"
                                            onkeyup="this.value=this.value.trim();" />
                                    </div>

                                    <div id="Main_uplBornDate">

                                        <div class="fieldContent">
                                            <label for="" class="hasSelect">出生日期</label>
                                            <div class="fieldSelect">
                                                <select name="ctl00$Main$cboBornYear"
                                                    onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$Main$cboBornYear\&#39;,\&#39;\&#39;)&#39;, 0)"
                                                    id="Main_cboBornYear" class="m-4 s-4 x-4">
                                                    <option selected="selected" value="">請選擇</option>
                                                    <option value="2002">民國 91 年</option>
                                                    <option value="2001">民國 90 年</option>
                                                    <option value="2000">民國 89 年</option>
                                                    <option value="1999">民國 88 年</option>
                                                    <option value="1998">民國 87 年</option>
                                                    <option value="1997">民國 86 年</option>
                                                    <option value="1996">民國 85 年</option>
                                                    <option value="1995">民國 84 年</option>
                                                    <option value="1994">民國 83 年</option>
                                                    <option value="1993">民國 82 年</option>
                                                    <option value="1992">民國 81 年</option>
                                                    <option value="1991">民國 80 年</option>
                                                    <option value="1990">民國 79 年</option>
                                                    <option value="1989">民國 78 年</option>
                                                    <option value="1988">民國 77 年</option>
                                                    <option value="1987">民國 76 年</option>
                                                    <option value="1986">民國 75 年</option>
                                                    <option value="1985">民國 74 年</option>
                                                    <option value="1984">民國 73 年</option>
                                                    <option value="1983">民國 72 年</option>
                                                    <option value="1982">民國 71 年</option>
                                                    <option value="1981">民國 70 年</option>
                                                    <option value="1980">民國 69 年</option>
                                                    <option value="1979">民國 68 年</option>
                                                    <option value="1978">民國 67 年</option>
                                                    <option value="1977">民國 66 年</option>
                                                    <option value="1976">民國 65 年</option>
                                                    <option value="1975">民國 64 年</option>
                                                    <option value="1974">民國 63 年</option>
                                                    <option value="1973">民國 62 年</option>
                                                    <option value="1972">民國 61 年</option>
                                                    <option value="1971">民國 60 年</option>
                                                    <option value="1970">民國 59 年</option>
                                                    <option value="1969">民國 58 年</option>
                                                    <option value="1968">民國 57 年</option>
                                                    <option value="1967">民國 56 年</option>
                                                    <option value="1966">民國 55 年</option>
                                                    <option value="1965">民國 54 年</option>
                                                    <option value="1964">民國 53 年</option>
                                                    <option value="1963">民國 52 年</option>
                                                    <option value="1962">民國 51 年</option>
                                                    <option value="1961">民國 50 年</option>
                                                    <option value="1960">民國 49 年</option>
                                                    <option value="1959">民國 48 年</option>
                                                    <option value="1958">民國 47 年</option>
                                                    <option value="1957">民國 46 年</option>
                                                    <option value="1956">民國 45 年</option>
                                                    <option value="1955">民國 44 年</option>
                                                    <option value="1954">民國 43 年</option>
                                                    <option value="1953">民國 42 年</option>
                                                    <option value="1952">民國 41 年</option>
                                                    <option value="1951">民國 40 年</option>
                                                    <option value="1950">民國 39 年</option>
                                                    <option value="1949">民國 38 年</option>
                                                    <option value="1948">民國 37 年</option>
                                                    <option value="1947">民國 36 年</option>
                                                    <option value="1946">民國 35 年</option>
                                                    <option value="1945">民國 34 年</option>
                                                    <option value="1944">民國 33 年</option>
                                                    <option value="1943">民國 32 年</option>
                                                    <option value="1942">民國 31 年</option>
                                                    <option value="1941">民國 30 年</option>
                                                    <option value="1940">民國 29 年</option>
                                                    <option value="1939">民國 28 年</option>
                                                    <option value="1938">民國 27 年</option>
                                                    <option value="1937">民國 26 年</option>
                                                    <option value="1936">民國 25 年</option>
                                                    <option value="1935">民國 24 年</option>
                                                    <option value="1934">民國 23 年</option>
                                                    <option value="1933">民國 22 年</option>
                                                    <option value="1932">民國 21 年</option>

                                                </select>
                                                <select name="ctl00$Main$cboBornMonth"
                                                    onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$Main$cboBornMonth\&#39;,\&#39;\&#39;)&#39;, 0)"
                                                    id="Main_cboBornMonth" class="m-4 s-4 x-4">
                                                    <option selected="selected" value="">請選擇</option>

                                                </select>
                                                <select name="ctl00$Main$cboBornDay" id="Main_cboBornDay"
                                                    class="m-4 s-4 x-4 last">
                                                    <option selected="selected" value="">請選擇</option>

                                                </select>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="fieldContent">
                                        <label for="Main_txtMemberMobile">手機號碼</label>
                                        <input name="ctl00$Main$txtMemberMobile" type="text" maxlength="10"
                                            id="Main_txtMemberMobile"
                                            onkeydown="return (event.keyCode &lt; 48 || event.keyCode >57 || event.keyCode!=13);"
                                            placeholder="請輸入手機號碼" />
                                    </div>

                                    <div id="Main_uplSelectCityArea">

                                        <div class="fieldContent">
                                            <label for="" class="hasSelect">縣市/鄉鎮</label>
                                            <div class="fieldSelect">
                                                <select name="ctl00$Main$cboCity"
                                                    onchange="javascript:setTimeout(&#39;__doPostBack(\&#39;ctl00$Main$cboCity\&#39;,\&#39;\&#39;)&#39;, 0)"
                                                    id="Main_cboCity" class="m-6 s-6 x-6">
                                                    <option selected="selected" value="">請選擇</option>
                                                    <option value="台北市">台北市</option>
                                                    <option value="桃園市">桃園市</option>
                                                    <option value="新北市">新北市</option>
                                                    <option value="基隆市">基隆市</option>
                                                    <option value="苗栗縣">苗栗縣</option>
                                                    <option value="新竹市">新竹市</option>
                                                    <option value="新竹縣">新竹縣</option>
                                                    <option value="雲林縣">雲林縣</option>
                                                    <option value="彰化縣">彰化縣</option>
                                                    <option value="南投縣">南投縣</option>
                                                    <option value="台中市">台中市</option>
                                                    <option value="嘉義市">嘉義市</option>
                                                    <option value="嘉義縣">嘉義縣</option>
                                                    <option value="宜蘭縣">宜蘭縣</option>
                                                    <option value="台南市">台南市</option>
                                                    <option value="高雄市">高雄市</option>
                                                    <option value="屏東縣">屏東縣</option>
                                                    <option value="花蓮縣">花蓮縣</option>
                                                    <option value="台東縣">台東縣</option>
                                                    <option value="金門縣">金門縣</option>
                                                    <option value="連江縣">連江縣</option>
                                                    <option value="澎湖縣">澎湖縣</option>

                                                </select>
                                                <select name="ctl00$Main$cboArea" id="Main_cboArea"
                                                    class="m-6 s-6 x-6 last">
                                                    <option selected="selected" value="">請選擇</option>

                                                </select>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="fieldContent">
                                        <label for="Main_txtMemberAddress">地址</label>
                                        <input name="ctl00$Main$txtMemberAddress" type="text" id="Main_txtMemberAddress"
                                            onkeydown="return (event.keyCode!=13);" placeholder="請輸入地址" />
                                    </div>

                                    <div class="fieldContent">
                                        <label for="Main_txtMemberEmail">Email</label>
                                        <input name="ctl00$Main$txtMemberEmail" type="text" id="Main_txtMemberEmail"
                                            onkeydown="return (event.keyCode!=13);" placeholder="請輸入Email" />
                                    </div>

                                </div>
                                <!-- formField -->
                                <div class="conditionTerm">
                                    <ul id="tab" class="list-unstyled">
                                        <li class="x-12"><a href="#" @click.prevent="noticeTodo">網路保險及服務定型化契約</a></li>
                                        <li class="x-12"><a href="#" @click.prevent="noticeTodo">個資告知事項</a></li>
                                        <li class="x-12"><a href="#" @click.prevent="noticeTodo">注意事項</a></li>
                                    </ul>
                                </div>
                                <!-- /board -->

                                <div class="btn-checkbox m-12 s-12 x-12">
                                    <input id="Main_chkSpecification" type="checkbox"
                                        name="ctl00$Main$chkSpecification" />
                                    <label for="Main_chkSpecification">
                                        <i class="fa fa-check-circle"></i>
                                        <span class="tooltip"
                                            title="什麼是監護宣告？<br/><br/>對於精神障礙或其他心智缺陷，致不能為意思表示或受意思表示，或不能辨識其意思表示效果者，法院得依聲請人之聲請，為監護之宣告。">我已閱讀並同意網路保險及服務定型化契約，且未受有<u>
                                                <font color="#d80c1e">監謢宣告</font>
                                            </u>。</span>
                                    </label>
                                </div>

                                <div class="btn-checkbox m-12 s-12 x-12">
                                    <input id="Main_chkAgreeMemberSpecification" type="checkbox"
                                        name="ctl00$Main$chkAgreeMemberSpecification" />
                                    <label for="Main_chkAgreeMemberSpecification">
                                        <i class="fa fa-check-circle"></i>
                                        <span>
                                            <font color="#d80c1e">同意</font>通過驗證後會將您的會員資料「姓名、地址、電子郵件、手機」載入至要/被保人欄位中
                                        </span>
                                    </label>
                                </div>

                                <div class="btn-checkbox m-12 s-12 x-12">
                                    <input id="Main_chkJoinSK858AndInsuredService" type="checkbox"
                                        name="ctl00$Main$chkJoinSK858AndInsuredService" />
                                    <label for="Main_chkJoinSK858AndInsuredService">
                                        <i class="fa fa-check-circle"></i>
                                        <span>加入網路投保與網路保險服務，可免要保書簽名回傳手續及查詢投保資料及專屬服務</span>
                                    </label>
                                </div>

                                <div class="btn-checkbox m-12 s-12 x-12">
                                    <input id="Main_chkNewsLetter" type="checkbox" name="ctl00$Main$chkNewsLetter" />
                                    <label for="Main_chkNewsLetter">
                                        <i class="fa fa-check-circle"></i>
                                        <span>我想透過E-mail不定期收到保險資訊等訊息</span>
                                    </label>
                                </div>

                            </div>
                            <!-- formSet -->
                        </div>
                        <!-- row -->
                        <div class="btnGroup">
                            <button type="reset" class="btn btnCancle" v-on:click="cancleClick">取消</button>
                            <button type="submit" class="btn btnPrimary" v-on:click="registerClick">註冊</button>
                        </div>
                    </div>
                    <!-- container -->
                </form>
            </main>
            <a class="goTop" href="#" title="Go to Top"><i class="fa fa-chevron-up"></i></a>
        </div>
    </div>
</template>

<script>
export default {
  name: 'Registered',
  data () {
    return {
      member: {
        uid: '',
        password: '',
        name: '',
        birthday: '',
        mobile: '',
        postalcode: '',
        city: '',
        area: '',
        address: '',
        email: '',
        otpcheck: '',
        qply: '',
        rememberemail: ''
      }
    }
  },
  methods: {
    registerClick () {
      console.log('registerClick')
      const api = `${process.env.VUE_APP_API_MEMBERCREATE}`
      console.log(api)
      this.$http.post(api, this.member)
        .then((res) => {
          console.log(res)
        })
    },
    cancleClick () {
      console.log('registerClick')
      this.$router.push('/')
    },
    noticeTodo() {
      alert('請實作！')
    }    
  }
}
</script>
