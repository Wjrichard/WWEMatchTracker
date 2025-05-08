import type { User } from '@/types/user.ts'
import {ref} from 'vue'
import axios from "axios";

export const initializedUser = {
    userId: 0,
    email: '',
    username: ''
}

export const _Users = ref<User[]>([]);
export const selectedUser = ref<User>(initializedUser);


export async function createUser(email:string,username:string){
    const newUser = initializedUser
    newUser.email = email
    newUser.username = username
    const response = await axios.post('https://localhost:44328/Users/AddUser',newUser)
}

export async function loadUsers() {
    try {
        const response = await axios.get('https://localhost:44328/Users/GetUsers');
        _Users.value = response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}


export async function setUser(email:string,username?:string) {
    const curUser = _Users.value.find(user => user.email === email)
    if (curUser){
        selectedUser.value = curUser
        return true
    }
    if (username && username !== '')
        await createUser(email,username)

    return false
}

const userStore = { _Users, selectedUser, loadUsers, initializedUser, setUser };
export default userStore;
