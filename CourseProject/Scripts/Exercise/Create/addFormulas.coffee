editor = null

window.onload = () ->
  editor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'en' })
  editor.insertInto(document.getElementById('editorContainer'))
  
$ ->
    window.onload()

    $('#addFormula').on 'click', () ->
      jsItem = document.createElement('p')
      jsItem.innerHTML = editor.getMathML()
      $('#formulas').append(jsItem)