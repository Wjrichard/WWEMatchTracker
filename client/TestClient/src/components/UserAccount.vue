<script setup lang="ts">
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/vue'
import { RouterLink } from 'vue-router'
import userStore from "@/stores/user.ts";
import { ref } from 'vue'

const openModal = ref(false)

</script>

<template>
    <Menu as="div" class="relative inline-block px-3 pt-2 text-left">
        <div>
            <MenuButton
                class="group w-full rounded-md bg-gray-200 px-3.5 py-2 text-left text-sm font-medium text-gray-700 hover:bg-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-100"
            >
        <span
            class="flex w-full items-center justify-between pl-1 space-x-3"
        >
          <span class="flex min-w-0 flex-1 flex-col">
            <span class="truncate text-sm font-medium text-gray-900">
                {{ userStore.selectedUser.value.username }} - {{ userStore.selectedUser.value.email }}
            </span>
            <span class="truncate text-xs text-gray-500">
                {{userStore.selectedUser}}
            </span>
          </span>
        </span>
            </MenuButton>
        </div>
        <Transition>
            <MenuItems
                class="w-100 absolute left-0 right-0 z-10 mx-3 mt-2 min-w-fit origin-top divide-y divide-gray-200 whitespace-nowrap rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
            >
                <div class="py-1">
                    <MenuItem v-slot="{ active }">
                        <button
                            class="w-full text-left"
                            :class="[active ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                    'block px-4 py-2 text-sm']"
                        >
                            test
                        </button>
                    </MenuItem>
                </div>
                <div class="py-1">
                    <MenuItem v-slot="{ active }">
                        <RouterLink to="/"
                            :class="[active ? 'bg-gray-100 text-gray-900' : 'text-gray-700',
                                    'block px-4 py-2 text-sm']"
                        >
                            Sign In
                        </RouterLink>
                    </MenuItem>
                    <MenuItem v-slot="{ active }">
                        
                    </MenuItem>
                </div>
            </MenuItems>
        </Transition>
    </Menu>
</template>
