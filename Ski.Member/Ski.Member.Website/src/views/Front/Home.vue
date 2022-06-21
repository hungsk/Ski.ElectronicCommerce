<template>
  <div class="home">
    <header class="page-row">
        <div class='topHead'>
            <div class='container'>
                <div class='row'><div class='toggle hidden-mdlg'>
                    <img src='https://www.sk858.com.tw/member/images/toggleMenu.png' class='img-responsive'>
                    </div>
                    <div class='headLogo'>
                        <a href='https://www.sk858.com.tw/'><img src='https://www.sk858.com.tw/member/images/logo.png' alt='新光產險'></a>
                    </div>
                    <div class='headContact hidden-smxs'>
                        <ul class='list-unstyled list-inline'>
                            <li><a href='mailto://skservice@skinsurance.com.tw'><i class='fa fa-envelope'></i> 客服信箱</a></li>
                            <li><a href='tel://(02)2517-5725'><i class='fa fa-phone'></i> (02)2517-5725</a></li>
                            <li><a href='https://www.skinsurance.com.tw' title='新光產險官網'><i class='fa fa-building'></i>新光產險</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!-- 主頁面選單 -->
        <nav class='offcanvas'>
            <div class='container'>
                <ul class='list-unstyled' id='navDropdown'>
                    <li><a href='https://www.sk858.com.tw/' title='回首頁投保'><i class='fa fa-home'></i> 回首頁投保</a></li>
                    <li><router-link to="/"><i class='fa fa-user'></i>會員專區</router-link></li>
                    <li><router-link to="/registered"><i class='fa fa-plus-circle'></i>加入會員</router-link></li>
                    <li><a href='Login.aspx' title='會員登入'><i class='fa fa-sign-in'></i> 會員登入</a></li>
                </ul>
            </div>
        </nav>
    </header>
    <router-view></router-view>
    <footer class="footer">
      <div class="footerWrap">
        <div class="container">
          <div class="row">
            <div class="footerLogo"><img src="https://www.sk858.com.tw/member/images/logo-w-iso.png" alt="新光產險"
                class="img-responsive"></div>
            <div class="footerContact">
              <ul class="list-unstyled" align="left">
                <li>
                  <a href="https://line.me/R/ti/p/%40hgk5818n"><img height="30" alt="加入好友"
                      src="https://www.sk858.com.tw/member/images/line-friend.png" title="加入LINE好友"></a>
                  <p></p>
                  <a href='javascript: void(window.open(&apos;https://lineit.line.me/share/ui?url=&apos; .concat(encodeURIComponent(location.href)) ));'
                    title="分享給 LINE 好友"><img src="https://www.sk858.com.tw/member/images/line-share.png" height="30"
                      alt="LINE分享給好友"></a>
                </li>
                <p></p>
                <li><i class="fa fa-phone"></i> 諮詢專線：<a href="tel:(02)2517-5725" title="諮詢專線">(02)2517-5725</a></li>
                <li><i class="fa fa-fax"></i> 傳真號碼：(02)2507-6810</li>
                <li><i class="fa fa-envelope-o"></i> 服務信箱：<a href="mailto:skservice@skinsurance.com.tw"
                    title="客服信箱">skservice@skinsurance.com.tw</a></li>
                <li class="hidden-smxs"><i class="fa fa-clock-o"></i> 服務時間：週一~週五 8:30~12:00，13:30~18:00(一般上班日)</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'Home',
  components: {
  },
  data() {
    return {
      LoadingStatus: false
    }
  }
}
</script>
