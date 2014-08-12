editor = null

window.onload = () ->
  editor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'en' })
  editor.insertInto(document.getElementById('editorContainer'))
  
$ ->
    window.onload()

    $('#addFormula').on 'click', (e) ->
      e.stopPropagation()
      e.preventDefault()
      item = document.createElement('p')
      item.id = "formula"
      item.innerHTML = editor.getMathML()
      $('#formulas').append(item)