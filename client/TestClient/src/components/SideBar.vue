<script setup lang="ts">
import userStore from "@/stores/user.ts";
import UserAccount from "@/components/UserAccount.vue";
import { RouterLink, type RouterLinkProps, type RouteRecordRaw } from 'vue-router'

</script>

<template>
    <div class="fixed inset-y-0 flex w-64 flex-none flex-col border-r bg-gray-100 lg:border-gray-200">
        <div class="flex bg-gray-200">
            <div class="flex items-center justify-between">
                <router-link
                    to="/"
                    class="flex h-14 items-center border-b px-10 text-center ring-inset ring-blue-500 hover:bg-gray-200 focus:bg-gray-200 focus:outline-none focus:ring-2"
                >
                    <h1 class="text-warm-gray-900 text-2xl font-extrabold tracking-tight">Match Tracker</h1>
                </router-link>
            </div>
        </div>
        <div class="mt-4 flex h-full flex-1 flex-col">
            <div>
                <UserAccount />
            </div>
            <nav class="h-0 grow overflow-y-auto scrollbar-simple my-4 ml-3">
                <ul>
                    <li>
                        <RouterLink to="/">Sign In</RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/events">Event List</RouterLink>
                    </li>
                    <li>
                        <RouterLink to="/predictions">Prediction List</RouterLink>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</template>
