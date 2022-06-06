export function pagination (data, outputData, Page = 1) {
  const dataTotal = data.length
  const perPage = 12 // 每頁資料筆數
  const showPage = 3 // 每頁頁碼數
  let pageTotal = Math.ceil(dataTotal / perPage)
  if (pageTotal === 0) {
    pageTotal = 1
  }
  let currentPage = Page
  if (currentPage > pageTotal) {
    currentPage = pageTotal
  }
  const minData = currentPage * perPage - perPage + 1
  const maxData = currentPage * perPage
  outputData = []
  data.forEach((item, index) => {
    const num = index + 1
    if (num >= minData && num <= maxData) {
      outputData.push(item)
    }
  })
  // 分頁一頁顯示3筆頁碼
  let pageItem = {}
  pageItem.pageCurrent = []
  const PageTagTotal = Math.ceil(pageTotal / showPage)
  const currentPageTag = Math.ceil(Page / showPage)
  const pageCurrentItem = []
  const minPage = currentPageTag * showPage - showPage + 1
  const maxPage = currentPageTag * showPage
  for (let i = 1; i < pageTotal + 1; i++) {
    if (i >= minPage && i <= maxPage) {
      pageCurrentItem.push(i)
    }
  }
  pageItem = {
    pageTotal,
    currentPage,
    hasPre: currentPage > 1,
    hasNext: currentPage < pageTotal,
    showPage: showPage,
    pageCurrent: Array.from(pageCurrentItem),
    currentPageTag,
    PageTagTotal,
    pageTagHasPre: currentPageTag > 1,
    pageTagHasNext: currentPageTag < PageTagTotal
  }
  return [pageItem, outputData]
}
