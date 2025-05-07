export type ModalResponseType = 'none' | 'submit' | 'cancel' | 'acknowledge'

export class ModalResponse {
    type: ModalResponseType = 'none'
    data: any

    constructor(type?: ModalResponseType, data?: any) {
        if (type) {
            this.type = type
        }
        if (data) {
            this.data = data
        }
    }
}
