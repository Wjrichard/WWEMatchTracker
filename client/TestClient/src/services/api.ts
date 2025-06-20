import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:44328/',
    headers: {
        'Content-Type': 'application/json'
    }
});

function getAllUsers() {
    const users = apiClient.get('/GetUsers');
    return users;
}

const api = {getAllUsers};
export default api

