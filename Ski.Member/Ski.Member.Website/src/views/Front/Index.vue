<template>
    <div class="loader">
        <h2>
            資料正在處理中，請稍後。<br />
            <br />
            (勿關閉頁面與重新整理，交易可能造成失敗、重覆)
        </h2>
    </div>
    <!-- 主頁面Header -->
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
                    <li><a href='Default.aspx' title='會員專區'><i class='fa fa-user'></i> 會員專區</a></li>
                    <li><a href='Registered.aspx' title='加入會員'><i class='fa fa-plus-circle'></i> 加入會員</a></li>
                    <li><a href='Login.aspx' title='會員登入'><i class='fa fa-sign-in'></i> 會員登入</a></li>
                </ul>
            </div>
        </nav>
    </header>
    <!-- 子頁面內容 -->
    <div class="canvasWrap">
        <div class="innerCanvas">
            <div class="overlay">
            </div>
            <main>
                <form method="post" action="./" id="form1">
                    <!-- content -->
                    <div class="canvasWrap">
                        <div class="innerCanvas">
                            <div class="overlay"></div>
                            <div class="container">
                                <div class="Wrap">

                                    <div class="container">
                                        <div class="pageTitle login">會員專區</div>
                                        <span id="Main_lbMemberLoginTime" style="font-weight:bold;"></span>
                                    </div>
                                    <!-- List -->
                                    <div class="row">

                                        <div class="portalType m-3 s-12">
                                            <table>
                                                <tr>
                                                    <td><i class="fa fa-user"></i>&nbsp;會員服務</td>
                                                </tr>
                                            </table>
                                            <ul>
                                                <li><a href='Registered.aspx' title='加入會員'>加入會員</a></li>
                                                <li><a href='Login.aspx' title='會員登入'>會員登入</a></li>
                                                <li><a href='https://www.sk858.com.tw/' title='回首頁投保'>回首頁投保</a></li>
                                            </ul>
                                        </div>

                                        <div class="portalType m-3 s-12">
                                            <table>
                                                <tr>
                                                    <td><i class="fa fa-search"></i>&nbsp;保單服務</td>
                                                </tr>
                                            </table>
                                            <ul>
                                                <li><a href="MemberServices/QueryPolicy.aspx" title="保單查詢、變更、申請、補發">保單查詢、變更、申請、補發</a></li>
                                                <li><a href="https://www.skinsurance.com.tw/WebQueryPolicy/CarQyueryEIcard.aspx" target="_blank" rel="noopener noreferrer" title="強制險電子式憑證">強制險電子式憑證</a></li>
                                                <li><a href="javascript:void(0)" class="AlertBuild" title="旅綜險英文投保證明">旅綜險英文投保證明</a></li>
                                            </ul>
                                        </div>

                                        <div class="portalType m-3 s-12">
                                            <table>
                                                <tr>
                                                    <td><i class="fa fa-shield"></i>&nbsp;理賠服務</td>
                                                </tr>
                                            </table>
                                            <ul>
                                                <li><a href="CLMCAR/CarDeclare.aspx" target="_blank" rel="noopener noreferrer" title="車險理賠線上填單">車險理賠線上填單</a></li>
                                                <li><a href="CLMCAR/ClmCarProgress.aspx" target="_blank" rel="noopener noreferrer" title="各險理賠進度查詢">各險理賠進度查詢</a></li>
                                                <li><a href="https://www.skinsurance.com.tw/ski/Doc.aspx?uID=1&sID=&ST=" target="_blank" rel="noopener noreferrer" title="各險理賠申請書下載">各險理賠申請書下載</a></li>
                                                <li><a href="https://www.skinsurance.com.tw/ski/Location.aspx" target="_blank" rel="noopener noreferrer" title="服務據點">服務據點</a></li>
                                                <li><a href="tel:0800-789-999" title="理賠專線">理賠專線0800-789-999</a></li>
                                            </ul>
                                        </div>

                                        <div class="portalType m-3 s-12 last">
                                            <table>
                                                <tr>
                                                    <td><i class="fa fa-money"></i>&nbsp;繳費服務</td>
                                                </tr>
                                            </table>
                                            <ul>
                                                <li><a href="https://www.sk858.com.tw/payrwd/PayMent.aspx" target="_blank" rel="noopener noreferrer" title="手機條碼超商繳費">手機條碼超商繳費</a></li>
                                                <li><a href="https://my.taishinbank.com.tw/Payment.jsp" target="_blank" rel="noopener noreferrer" title="台新會員繳費">台新會員繳費</a></li>
                                                <li><a href="https://www.sk858.com.tw/payrwd/PayMent.aspx" target="_blank" rel="noopener noreferrer" title="列印繳款單">列印繳款單</a></li>
                                                <li><a href="https://www.sk858.com.tw/payrwd/PayMent.aspx" target="_blank" rel="noopener noreferrer" title="線上繳費">線上繳費</a></li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                                <!-- /Wrap -->
                            </div>
                        </div>
                    </div>
                </form>
            </main>
            <!-- 主頁面尾 -->
            <footer>
                <div class="footerWrap">
                    <div class="container">
                        <div class="row">
                            <div class="footerLogo"><img src="https://www.sk858.com.tw/member/images/logo-w-iso.png" alt="新光產險" class="img-responsive"></div>
                            <div class="footerContact">
                                <ul class="list-unstyled" align="left">
                                    <li>
                                        <a href="https://line.me/R/ti/p/%40hgk5818n"><img height="30" alt="加入好友" src="https://www.sk858.com.tw/member/images/line-friend.png" title="加入LINE好友"></a>
                                        <p></p>
                                        <a href='javascript: void(window.open(&apos;https://lineit.line.me/share/ui?url=&apos; .concat(encodeURIComponent(location.href)) ));' title="分享給 LINE 好友"><img src="https://www.sk858.com.tw/member/images/line-share.png" height="30" alt="LINE分享給好友"></a>
                                    </li>
                                    <p></p>
                                    <li><i class="fa fa-phone"></i> 諮詢專線：<a href="tel:(02)2517-5725" title="諮詢專線">(02)2517-5725</a></li>
                                    <li><i class="fa fa-fax"></i> 傳真號碼：(02)2507-6810</li>
                                    <li><i class="fa fa-envelope-o"></i> 服務信箱：<a href="mailto:skservice@skinsurance.com.tw" title="客服信箱">skservice@skinsurance.com.tw</a></li>
                                    <li class="hidden-smxs"><i class="fa fa-clock-o"></i> 服務時間：週一~週五 8:30~12:00，13:30~18:00(一般上班日)</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
            <a class="goTop" href="#" title="Go to Top"><i class="fa fa-chevron-up"></i></a>
        </div>
        <!-- innerCanvas -->
    </div>
    <!-- canvasWrap -->
</template>

<script>
export default {
  name: 'Index',
  components: {
  }
}
</script>