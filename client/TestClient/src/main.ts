import './assets/main.css'
import {loadUsers,_Users} from "@/stores/user.ts";
import { createApp } from 'vue'
import App from './App.vue'

const app = createApp(App)

await Promise.all([
    await loadUsers(),
    //console.log(_Users.value)
])


app.mount('#app')