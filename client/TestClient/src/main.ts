import './assets/main.css'
import {loadUsers} from "@/stores/user.ts";
import {loadMatches} from "@/stores/matches.ts";
import {loadEvents} from "@/stores/events.ts"
import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index.ts'

const app = createApp(App)
app.use(router)

await Promise.all([
    await loadUsers(),
    await loadMatches(),
    await loadEvents(),
])


app.mount('#app')