<template>
  <div class="d-none d-md-block row py-4">
    <div class="col-md-12">
      <nav class="navbar navbar-expand navbar-front navbar-front-hr" aria-label="navbar">
        <div class="container-fluid">
          <div class="collapse navbar-collapse">
            <ul class="navbar-nav me-auto w-100 justify-content-between align-items-center">
              <li class="nav-item">
                <router-link class="nav-link" aria-current="page" to="/">首頁</router-link>
              </li>
              <li class="nav-item dropdown position-static">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown"
                aria-expanded="false">
                  建材
                </a>
                <ul class="dropdown-menu w-100 shadow rounded" aria-labelledby="navbarDropdown">
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/material/大理石">大理石</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/material/木皮">木皮</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/material/表面材">表面材</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/material/磁磚">磁磚</router-link>
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
                    <router-link class="dropdown-item mb-0" to="/product/paint/壁紙">壁紙</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/paint/油漆">油漆</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/paint/特殊漆">特殊漆</router-link>
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
                    <router-link class="dropdown-item mb-0" to="/product/floor/木地板">木地板</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/floor/石塑地板">石塑地板</router-link>
                  </li>
                  <li class="mx-3 my-2">
                    <router-link class="dropdown-item mb-0" to="/product/floor/塑膠地板">塑膠地板</router-link>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Menu'
}
</script>
