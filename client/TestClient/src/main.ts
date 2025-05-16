import './assets/main.css'
import {loadUsers} from "@/stores/user.ts";
import {loadEvents} from "@/stores/events.ts"
import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index.ts'

const app = createApp(App)
app.use(router)

await Promise.all([
    await loadUsers(),
    await loadEvents(),
])


app.mount('#app')