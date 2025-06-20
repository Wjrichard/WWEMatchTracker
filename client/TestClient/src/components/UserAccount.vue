<script setup lang="ts">
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/vue'
import { RouterLink, useRouter } from 'vue-router'
import userStore from "@/stores/user.ts";
import { ref } from 'vue'

const openModal = ref(false)
const $router = useRouter()

function signOut() {
  userStore.selectedUser.value = undefined;
  localStorage.clear();
  $router.push('/');
}
function signInAndFocus() {
  $router.push('/').then(() => {
    setTimeout(() => {
      const emailInput = document.querySelector('input[type="email"], input[name="email"], input[placeholder*="Email" i]');
      if (emailInput) (emailInput as HTMLInputElement).focus();
    }, 100);
  });
}

</script>

<template>
  <div>
    <Menu as="div" class="relative w-full inline-block px-3 pt-2 text-left">
      <div>
        <MenuButton
          class="group w-full rounded-md bg-gray-200 px-3.5 py-2 text-left text-sm font-medium text-gray-700 hover:bg-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 focus:ring-offset-gray-100"
          @click="userStore.selectedUser?.value?.userId <= 0 ? signInAndFocus() : null"
        >
          <span class="flex w-full items-center justify-between pl-1 space-x-3">
            <span class="flex min-w-0 text-sm flex-1 w-full">
              <span class="divide-y divide-gray-200 w-full flex flex-col">
                <span class="truncate font-medium text-gray-900">
                  {{
                    (!userStore.selectedUser.value || userStore.selectedUser.value.userId <= 0)
                      ? 'Not Logged In.\n Click Here.'
                      : userStore.selectedUser.value.username
                  }}
                </span>
                <span v-if="userStore.selectedUser.value && userStore.selectedUser.value.userId > 0" class="truncate text-gray-500">
                  {{ userStore.selectedUser.value.email }}
                </span>
              </span>
            </span>
          </span>
        </MenuButton>
      </div>
      <Transition>
        <MenuItems
          class="w-100 absolute left-0 right-0 z-10 mx-3 mb-2 min-w-fit origin-bottom divide-y divide-gray-200 whitespace-nowrap rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none bottom-full"
          style="top: auto; bottom: 100%;"
        >
          <div class="py-1">
            <MenuItem v-if="userStore.selectedUser.value && userStore.selectedUser.value.userId > 0" v-slot="{ active }">
              <button
                class="w-full text-left"
                :class="[active ? 'bg-gray-100 text-gray-900' : 'text-gray-700', 'block px-4 py-2 text-sm']"
                @click="signOut"
              >
                Sign Out
              </button>
            </MenuItem>
            <MenuItem v-else v-slot="{ active }">
              <button
                class="w-full text-left"
                :class="[active ? 'bg-gray-100 text-gray-900' : 'text-gray-700', 'block px-4 py-2 text-sm']"
                @click="signInAndFocus"
              >
                Sign In
              </button>
            </MenuItem>
          </div>
        </MenuItems>
      </Transition>
    </Menu>
  </div>
</template>
