$ ->
    createParentDiv = () ->
        parent = document.createElement('div')
        parent.className = 'col-md-12'
        return parent

    createChildDiv = () ->
        childDiv = document.createElement('div')
        childDiv.className = 'col-md-4'
        return childDiv
    
    createInput = (name) ->
        input = document.createElement('input')
        input.type = 'text'
        input.name = name
        return input

    createButton = (name,text) ->   
        button = document.createElement('button')
        button.name = name
        button.innerText = text           
        return button         

    $jqXHRData = null

    createImageElement = (src) ->
        parent = createParentDiv()
        childImage = document.createElement('img')
        childImage.src = src
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-4 thumbnail"
        childDivForButton = createChildDiv()    
        childDivForImage.appendChild childImage
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton
        return parent

    createFormulaElement = (formula) ->
        parent = createParentDiv()
        childFormula = document.createElement('p')
        childFormula.innerHTML = formula
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-4"
        childDivForButton = createChildDiv()    
        childDivForImage.appendChild childFormula
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton
        return parent    
    
    editor = null

    window.onload = () ->
        editor = com.wiris.jsEditor.JsEditor.newInstance({ 'language': 'en' })
        editor.insertInto(document.getElementById('editorContainer'))    
        window.editor = editor
    
    initFileUpload = () ->
      $('#imageupload').fileupload({
        url: '/Exercise/UploadImage',
        dataType: 'json',
        add: (e, data) -> 
            $jqXHRData = data
        done: (event, data) -> 
            alert "file uploaded"
            jsItem = createImageElement(data.result.path)
            $('#listOfPictures').append(jsItem)
        fail: (event, data) ->
            alert "ERROR"
            if data.files[0].error
              alert data.files[0].error
      })

    $equations = []

    $images = []

    $tags = []

    $answers = []
#
#    getFormulas = () ->
#      elements = $("#listOfFormulas > #formula")
#      $.each elements, (e,value) ->
#        $equations.push(value.innerHTML)
#      return $equations
 
    getImages = () ->
      im = $("#listOfPictures > .row > .thumbnail > img") 
      $.each im, (e,val) ->
        $images.push(val.src)
      $images  

    $(document).ready () ->
        window.onload()
        $('#addFormula').on 'click', (e) ->
            e.stopPropagation()
            e.preventDefault()
            item = createFormulaElement(editor.getMathML())
            $('#listOfFormulas').append(item)
        $(document).on 'click', '#addAnswer', (e)->
            e.stopPropagation()
            e.preventDefault()
            createAnswerInput()
            return null
        $(document).on 'click', "[name='delete']", (e) ->
            e.stopPropagation()
            e.preventDefault()
            $(this).unbind()
            parent1 = $(this).parent()
            parent2 = parent1.parent()
            parent2.remove()
            return null
        $(document).on 'click', "#addTag", (e) ->
            e.stopPropagation()
            e.preventDefault()
            createTagInput()
            $("[data-autocomplete-source]").each () ->
                target = $(this)
                target.autocomplete({ source: target.attr("data-autocomplete-source") })
            return null
        $("[data-autocomplete-source]").each () ->
            target = $(this)
            target.autocomplete({ source: target.attr("data-autocomplete-source") })
            return null
        initFileUpload()
        $(document).on 'click', '#Upload', (e) ->
            e.stopPropagation()
            e.preventDefault()
            if $jqXHRData
                alert "upload"
                $jqXHRData.submit()
            return false
        $("#addVideo").on 'click', (e) ->
            e.stopPropagation()
            e.preventDefault()
            parent = createParentDiv()
            parent.innerHTML =  $("#video").val()
            $("#listOfVideos").append parent
            $("#video").val("")
        $("#Submit").on 'click', (e) ->
            alert "submit"
            answers = $("#inputAnswers").textext()[0].hiddenInput().val()
#            formulas = getFormulas()
            images = getImages()
            tags = $("#inputTags").textext()[0].hiddenInput().val()
            $('input#Category').val($("select#Category").val())    
            $('input#Answers').val(answers)
#            $('input#Formulas').val(formulas)
            $('input#Pictures').val(images)
            $('input#Tags').val(tags)         
        return null