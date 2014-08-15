$ ->
    createParentDiv = () ->
        parent = document.createElement('div')
        parent.className = 'row'
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
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-4"
        childDivForButton = createChildDiv()    
        childDivForImage.innerHTML = formula
        childDivForImage.setAttribute('name','AddEquation')
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton        
        return parent    

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
    
    getFormulas = () ->
      elements = $("[name='AddEquation']")
      $.each elements, (e,value) ->
        img = value.children[0]
        $equations.push(img.src)
      return $equations
 
    getImages = () ->
      im = $("#listOfPictures > .row > .thumbnail > img") 
      $.each im, (e,val) ->
        $images.push(val.src)
      $images  
    
    $(document).ready () ->
        $(".md-header").css('height', '35px')
        $(".jumbotron")[0].style.setProperty("height","250px")
        $(document).on 'click', '#addFormula', (e)->
            e.stopPropagation()
            e.preventDefault()
            img = $('#equationToImg')[0]
            list = $("#listOfFormulas")[0]
            if img.value
                child = createFormulaElement(img.value)
                $('#equationToImg')[0].value = ""
                $('#equation')[0].src = ""
                $('#equationInput')[0].value = ""
                list.appendChild(child)
            return null
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
        initFileUpload()
        $(document).on 'click', '#Upload', (e) ->
            e.stopPropagation()
            e.preventDefault()
            if $jqXHRData
                alert "upload"
                $jqXHRData.submit()
            return false
        $("#Submit").on 'click', (e) ->
            answers = $("#inputAnswers").textext()[0].hiddenInput().val()
            images = getImages()
            equation = getFormulas()
            equation = JSON.stringify(equation)
            tags = $("#inputTags").textext()[0].hiddenInput().val()
            $('input#Answers')[0].value = answers
            $('input#Pictures')[0].value = images
            $('input#Tags')[0].value = tags
            $('input#Equations').val(equation)    
            $("input#Name")[0].value = $("#Exercise_Name").val()
            $("input#Text")[0].value = $("[name='Exercise.Text']").val()
        return null