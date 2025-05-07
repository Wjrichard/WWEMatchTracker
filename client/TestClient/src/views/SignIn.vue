<script setup lang="ts">
import { ref, onMounted } from 'vue';
import userStore, {_Users} from '@/stores/user.ts';
import ModalBase from "@/components/ModalBase.vue";
import Button from "@/components/ButtonComponent.vue"
import {UserCircleIcon} from "@heroicons/vue/24/solid";

const showCreateUserModal = ref(false)
const strEmail = ref('');
const strNewEmail = ref('')
const strNewUsername = ref('')

function setUser() {
    console.log(strEmail.value)
    if (_Users.value.find(user => {user.email === strEmail.value})){
        userStore.setUser(strEmail.value)
    } else {
        showCreateUserModal.value = true
    }
    userStore.setUser(strEmail.value)
}

</script>

<template>
    <div class="flex min-h-full flex-1 items-center justify-center px-4 py-12 sm:px-6 lg:px-8">
        <div class="w-full max-w-sm space-y-10">
            <div>
                <h2 class="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">Sign in</h2>
            </div>
                <div>
                    <div class="pt-2">
                        <input v-model="strEmail"
                               class="block w-full rounded-t-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:relative focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" placeholder="Email address" />
                    </div>
                </div>

                <div class="pt-3">
                    <button @click="setUser()"
                            class="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 
                                    font-semibold text-white hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 
                                    focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                    >
                        Sign in
                    </button>
                </div>
                <div>
                    Current User: {{userStore.selectedUser}}
                </div>
        </div>
    </div>


    <ModalBase :open="showCreateUserModal"
               size="small"
               @close="showCreateUserModal = false">
        <template #icon>
            <UserCircleIcon class="h-8 w-8 text-yellow-600" aria-hidden="true" />
        </template>
        <template #header> 
            <span class="pl-3 text-xl font-bold">
                Create New User 
            </span>
        </template>
        <template #body>
            <div class="py-6 flex w-full gap-4">
                <span class="w-1/2">
                    <input v-model="strNewEmail"
                           class="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:relative focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" 
                           placeholder="Email address" />
                </span>
                <span class="w-1/2">
                    <input v-model="strNewUsername"
                           class="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:relative focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" 
                           placeholder="Username" />
                </span>
                
            </div>
        </template>
        <template #footer>
            <Button size="extra-large"
                    class="px-24"
                    color="blue"
                    @click="userStore.setUser('')">
                Confirm
            </Button>
            <Button size="extra-large"
                    class="px-24"
                    @click="showCreateUserModal = false">
                Cancel
            </Button>
        </template>
    </ModalBase>
</template>
