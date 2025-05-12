import './assets/main.css'
import {loadUsers} from "@/stores/user.ts";
import {loadMatches, loadMatchParticipants} from "@/stores/matches.ts";
import {loadEvents} from "@/stores/events.ts"
import { createApp } from 'vue'
import App from './App.vue'
import router from './router/index.ts'

const app = createApp(App)
app.use(router)

await Promise.all([
    await loadUsers(),
    //await loadMatches(),
    await loadEvents(),
    //await loadMatchParticipants(1)
])


app.mount('#app')