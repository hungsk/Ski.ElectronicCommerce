<template>
    <div class="loader">
        <h2>
            資料正在處理中，請稍後。<br />
            <br />
            (勿關閉頁面與重新整理，交易可能造成失敗、重覆)
        </h2>
    </div>
    <!-- 子頁面內容 -->
    <div class="canvasWrap">
        <div class="innerCanvas">
            <div class="overlay">
            </div>
            <main>
                <form>
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
                                                <router-link to="/registered"><li>加入會員</li></router-link>
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