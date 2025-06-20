<script setup lang="ts">
import { ref, onMounted } from 'vue';
import userStore, {_Users} from '@/stores/user.ts';
import Dialog from "@/components/Dialogs/Dialog.vue";
import Button from "@/components/General/Button.vue"
import TextInput from "@/components/Forms/TextInput.vue"
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
    <div class="h-full">
      <div class="h-1/3 flex justify-center items-center text-6xl leading-tight font-extrabold">
        WWE Predictions
      </div>
      
      <div class="flex h-1/3 items-center justify-center">
        <div class="w-full max-w-sm space-y-10">
          <div>
            <div class="pt-2">
                <TextInput 
                    v-model="strEmail"
                    @keydown.enter="setUser()"
                    placeholder="Email Address"
                />
            </div>
              
          </div>

          <div class="pt-3">
            <div class="relative flex justify-center">
              <Dialog header="Sign Up" 
                      size="lg" 
                      :isSaveCancel="true" 
                      close-button-text="Sign Up"
                      saveRouteName="events">
                  <template #trigger>
                      <Button label="Sign In" 
                              variant="solid" 
                              color="secondary" 
                              full-width 
                              @click="setUser()"
                              :to="userStore.selectedUser.value?.userId > 0 ? { name: 'events' } : undefined"
                      />
                  </template>

                  <template #body>
                      <div class="flex w-full gap-4">
                          <TextInput
                              label="Email"
                              v-model="strNewEmail"
                              autocomplete="off"
                              placeholder="Enter your email"
                              class="w-1/2"
                          />
                          <TextInput
                              label="Username"
                              v-model="strNewUsername"
                              autocomplete="off"
                              placeholder="Enter a username"
                              class="w-1/2"
                          />
                      </div>
                      
                  </template>
              </Dialog>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>
