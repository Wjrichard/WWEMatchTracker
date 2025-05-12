<script setup lang="ts">
import { ref, onMounted } from 'vue';
import userStore, {_Users} from '@/stores/user.ts';
import ModalBase from "@/components/Layout/ModalBase.vue";
import Button from "@/components/Layout/ButtonComponent.vue"
import router from '@/router';
import {UserCircleIcon} from "@heroicons/vue/24/solid";
import {useRouter} from "vue-router";

const showCreateUserModal = ref(false)
const strEmail = ref('');
const strNewEmail = ref('')
const strNewUsername = ref('')

const $router = useRouter()

const validEmail = (email:string) => {
    return String(email)
        .toLowerCase()
        .match(
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        );
};

function setUser() {
    if (_Users.value.find(user => user.email === strEmail.value)){
        userStore.setUser(strEmail.value)
        $router.push({ name: 'events' });
    } else {
        strNewEmail.value = strEmail.value
        showCreateUserModal.value = true
    }
    userStore.setUser(strEmail.value)
}

async function applyNewUser(){
    await userStore.setUser(strNewEmail.value,strNewUsername.value)
    await userStore.loadUsers()
    showCreateUserModal.value = false
    await $router.push({ name: 'events' });
}

</script>

<template>
    <div class="h-screen">
      <div class="h-1/3 flex justify-center items-center text-6xl leading-tight font-extrabold">
        WWE Predictions
      </div>
      
      <div class="flex h-1/3 items-center justify-center">
        <div class="w-full max-w-sm space-y-10">
          <div>
            <h2 class="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">Sign in</h2>
          </div>
          <div>
            <div class="pt-2">
              <input v-model="strEmail"
                     @keydown.enter="setUser()"
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
            <br/>
            All Users: {{userStore._Users}}
          </div>
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
            <Button size="large"
                    class="px-16 hover:bg-gray-100"
                    :disabled="strNewEmail === '' || strNewUsername === '' || !validEmail(strNewEmail)"
                    :title="strNewEmail === '' && 'Enter an email' 
                            || strNewUsername === '' && 'Enter a username'
                            || !validEmail(strNewEmail) && 'Invalid email format'
                            || ''"
                    @click="applyNewUser()">
                Confirm
            </Button>
            <Button size="large"
                    class="px-16 hover:bg-gray-100"
                    @click="showCreateUserModal = false">
                Cancel
            </Button>
        </template>
    </ModalBase>
</template>
