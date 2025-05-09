

export enum FileTypes {
  DocxType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
  PdfType = 'application/pdf',
  XlsxType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
}

export class FileUtilities {
  static #downloadBlob(filename: string, blob: Blob) {
    const link = document.createElement('a')
    link.href = URL.createObjectURL(blob)
    link.download = filename
    link.target = '_blank'
    link.click()
    URL.revokeObjectURL(link.href)
  }

  static b64toBlob = (b64Data: string, contentType = '', sliceSize = 512) => {
    const byteCharacters = atob(b64Data)
    const byteArrays = []

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      const slice = byteCharacters.slice(offset, offset + sliceSize)

      const byteNumbers = new Array(slice.length)
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i)
      }

      const byteArray = new Uint8Array(byteNumbers)
      byteArrays.push(byteArray)
    }

    return new Blob(byteArrays, { type: contentType })
  }

  static fileToBase64 = async (file: File): Promise<string> => {
    return await new Promise<string>((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)

      reader.onload = () => {
        resolve(reader.result?.toString().split(',')[1] ?? '')
      }
      reader.onerror = (error) => {
        reject(error)
      }
    })
  }

  // TODO: find a way to download file without juggling encodings?
  static downloadBase64(b64Data: string, fileName: string, type: FileTypes) {
    const blob = FileUtilities.b64toBlob(b64Data, type)
    this.#downloadBlob(fileName, blob)
  }

  static downloadString(filename: string, str: string) {
    const blob = new Blob([str], { type: 'plain/text' })
    this.#downloadBlob(filename, blob)
  }

  static getExtensionFromName(fileName: string): string {
    return fileName.split('.').pop() ?? fileName
  }

  static removeExtensionFromName(fileName: string): string {
    // return unchanged if no period or begins with period
    if (fileName.indexOf('.') <= 0) return fileName

    return fileName.split('.').slice(0, -1).join('.')
  }

  /**
   * Determine mime type based on file signature (start of base64 string)
   *
   * @param base64
   */
  static getMimeTypeFromBase64(base64: string): string {
    const signatures = new Map<RegExp, string>([
      [/^\/9j\//, 'image/jpg'],
      [/^iVBORw0KGgo/, 'image/png'],
      [/^UEsDBBQABgA/, 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'],
      [/^JVBERi0/, 'application/pdf']
    ])

    for (const [signature, mime] of signatures) {
      if (signature.test(base64)) {
        return mime
      }
    }

    return ''
  }

}
