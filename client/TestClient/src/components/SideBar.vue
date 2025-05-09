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
                    class="flex h-auto items-center border-b px-10 text-center ring-inset ring-blue-500 hover:bg-gray-200 focus:bg-gray-200 focus:outline-none focus:ring-2"
                >
                    <h1 class="text-warm-gray-900 text-5xl py-4 font-extrabold tracking-tight">
                      Match Tracker
                    </h1>
                </router-link>
            </div>
        </div>
        <div class="mt-4 flex h-full flex-1 flex-col">
            <div>
                <UserAccount />
            </div>
            <nav class="h-0 grow overflow-y-auto scrollbar-simple ml-3">
                <ul class="divide-y divide-gray-200">
                    <li class="py-3">
                        <RouterLink 
                            to="/"
                            class="flex items-center justify-between text-2xl"
                        >
                          Sign In
                        </RouterLink>
                    </li>
                    <li class="py-3">
                      <RouterLink
                          to="/events"
                          class="flex items-center justify-between text-2xl"
                      >
                        Event List
                      </RouterLink>
                    </li>
                    <li class="py-3">
                      <RouterLink
                          to="/predictions"
                          class="flex items-center justify-between text-2xl"
                      >
                        Prediction List
                      </RouterLink>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
</template>
