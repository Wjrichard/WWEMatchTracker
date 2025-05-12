<script setup lang="ts">
import {RouterView} from "vue-router";
import SideBar from "@/components/SideBar.vue";
</script>

<template>
    <div id="app" class="h-full flex w-full">
        <aside class="w-1/5">
            <SideBar />
        </aside>
        <div class="w-4/5">
            <router-view />
        </div>
    </div>
</template>


