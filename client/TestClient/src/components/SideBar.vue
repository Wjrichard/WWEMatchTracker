<script setup lang="ts">
import userStore from "@/stores/user.ts";
import UserAccount from "@/components/UserAccount.vue";
import { RouterLink, type RouterLinkProps, type RouteRecordRaw } from 'vue-router'

</script>

<template>
    <div class="fixed inset-y-0 flex w-96 flex-none flex-col border-r bg-gray-100 lg:border-gray-200">
        <div class="flex w-full bg-gray-200">
            <div class="flex w-full items-center justify-between">
                <router-link
                    to="/"
                    class="flex h-auto w-full justify-center items-center border-b px-10 text-center ring-inset ring-blue-500 hover:bg-gray-200 focus:bg-gray-200 focus:outline-none focus:ring-2"
                >
                    <h1 class="text-warm-gray-900 text-5xl py-4 font-extrabold tracking-tight">
                        Navigation
                    </h1>
                </router-link>
            </div>
        </div>
        <div class="mt-4 flex w-full h-full flex-1 flex-col">
            <div>
                <UserAccount />
            </div>
            <nav class="h-0 grow overflow-y-auto scrollbar-simple ml-3">
                <ul class="divide-y divide-gray-200">
                    <RouterLink
                        to="/"
                        class="flex items-center justify-between text-2xl"
                    >
                        <li class="py-3">
                              Sign In
                        </li>
                    </RouterLink>
                    <RouterLink
                        to="/events"
                        class="flex items-center justify-between text-2xl"
                    >
                        <li class="py-3">
                            Event List
                        </li>
                    </RouterLink>
                    <RouterLink
                        to="/predictions"
                        class="flex items-center justify-between text-2xl"
                    >
                    <li class="py-3">
                        Prediction List
                    </li>
                    </RouterLink>
                </ul>
            </nav>
        </div>
    </div>
</template>
