<script setup lang="ts">
import {RouterLink,RouterView} from "vue-router";
import SideBar from "@/components/SideBar.vue";
</script>

<template>
    <div id="app" class="h-full flex w-full">
        <aside class="w-64">
            <SideBar />
        </aside>
        <main>
            <router-view />
        </main>
    </div>
</template>

<style>
    #app {
        display: flex;
        width: 100%;
        height: 100%;
        box-sizing: border-box;
        padding: 0;
        margin: 0;
    }
</style>

