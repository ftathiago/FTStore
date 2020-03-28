import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { ProductComponent } from "./product/register/product.register.component";
import { LoginComponent } from "./user/login/login.component";
import { RouteGuard } from "./authorization/route.guard";
import { UserService } from "./services/user/user.service";
import { ProdutoService } from "./services/product/product.service";
import { UserRegisterComponent } from "./user/register/user.register.component";
import { ProductSearchComponent } from "./product/search/product.search.component";
import { LojaVitrineComponent } from "./store/showcase/store.showcase.component";
import { TruncateModule } from "ng2-truncate";
import { LojaProdutoComponent } from "./store/product-details/store.product-details.component";
import { LojaEfetivarComponent } from "./store/show-cart/store.show-cart.component";
import { Nl2BrPipeModule } from "nl2br-pipe";
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductComponent,
    LoginComponent,
    UserRegisterComponent,
    ProductSearchComponent,
    LojaVitrineComponent,
    LojaProdutoComponent,
    LojaEfetivarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    TruncateModule,
    Nl2BrPipeModule,
    Ng2SearchPipeModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "product", component: ProductComponent, canActivate: [RouteGuard] },
      { path: "signin", component: LoginComponent },
      { path: "user-register", component: UserRegisterComponent },
      { path: "product-search", component: ProductSearchComponent },
      { path: "store-product-details", component: LojaProdutoComponent },
      { path: "store-cart", component: LojaEfetivarComponent, canActivate: [RouteGuard] }
    ])
  ],
  providers: [UserService, ProdutoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
